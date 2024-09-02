using System;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenPresenter : MonoBehaviour
{
    [SerializeField] private MainScreenView _mainScreenView;
    [SerializeField] private AddTripScreenPresenter _addTripScreen;
    [SerializeField] private FilledHistoryWindow _firstFilledWindow;
    [SerializeField] private FilledHistoryWindow _secondFilledWindow;
    [SerializeField] private FilledHistoryWindow _thridFilledWindow;

    private DataSaver _saver;
    private int _totalPrice = 0;
    private int _totalFuel = 0;
    private List<FilledHistoryWindow> _filledHistoryWindows;
    private List<int> _availableWindowIndices = new List<int>();
    private bool _disabledHistoryText;
    private bool _onboardingSeen;

    public event Action AddTripClicked;
    public event Action CalculatorClicked;
    public event Action SettingsClicked;
    public event Action OnboardingCompleted;
    public event Action<FilledHistoryWindow> SeeMoreButtonClicked;

    public bool OnboardingSeen => _onboardingSeen;

    private void Start()
    {
        _saver = new DataSaver();
        _mainScreenView.Disable();
        _filledHistoryWindows = new List<FilledHistoryWindow>()
            { _firstFilledWindow, _secondFilledWindow, _thridFilledWindow };

        _mainScreenView.SetTotalValues(_totalPrice, _totalFuel);

        LoadData();
        
        if(_onboardingSeen)
            OnboardingCompleted?.Invoke();

        for (int i = 0; i < _filledHistoryWindows.Count; i++)
        {
            _filledHistoryWindows[i].Deleted += ProcessFilledWindowDeletion;
            _filledHistoryWindows[i].SeeMorePressed += ProcessSeeMoreClicked;
            _filledHistoryWindows[i].TripDataUpdated += ProcessDataEdited;
            _availableWindowIndices.Add(i);
        }
    }

    private void OnEnable()
    {
        _mainScreenView.AddTripButtonClicked += ProcessAddTripButtonClicked;
        _mainScreenView.CalculatorButtonClicked += ProcessCalculatorClicked;
        _mainScreenView.SettingsButtonClicked += ProcessSettingButtonClicked;

        _addTripScreen.FullTripDataSaved += ProcessFullTripDataProvided;
    }

    private void OnDisable()
    {
        _mainScreenView.AddTripButtonClicked -= ProcessAddTripButtonClicked;
        _mainScreenView.CalculatorButtonClicked -= ProcessCalculatorClicked;

        _addTripScreen.FullTripDataSaved -= ProcessFullTripDataProvided;

        foreach (var window in _filledHistoryWindows)
        {
            window.Deleted -= ProcessFilledWindowDeletion;
            window.SeeMorePressed -= ProcessSeeMoreClicked;
            window.TripDataUpdated -= ProcessDataEdited;
        }
    }

    public void ShowMenuScreen()
    {
        if (!_onboardingSeen)
        {
            _onboardingSeen = true;
            SaveData();
        }

        _mainScreenView.Enable();
    }

    private void ProcessDataEdited(FilledHistoryWindow editedWindow)
    {
        SaveData();
        _totalPrice -= editedWindow.PreviousTotalPrice;
        _totalFuel -= editedWindow.PreviousTotalFuel;
        _totalPrice += editedWindow.TotalPrice;
        _totalFuel += editedWindow.TotalFuel;
        _mainScreenView.SetTotalValues(_totalPrice, _totalFuel);
    }

    private void UpdateTotalValues(FilledHistoryWindow editedWindow)
    {
        _totalPrice += editedWindow.TotalPrice;
        _totalFuel += editedWindow.TotalFuel;
        _mainScreenView.SetTotalValues(_totalPrice, _totalFuel);
    }

    private void ProcessSeeMoreClicked(FilledHistoryWindow filledWindow)
    {
        SeeMoreButtonClicked?.Invoke(filledWindow);
        _mainScreenView.Disable();
    }

    private void ProcessFilledWindowDeletion(FilledHistoryWindow filledWindow)
    {
        int windowIndex = _filledHistoryWindows.IndexOf(filledWindow);

        if (windowIndex >= 0 && !_availableWindowIndices.Contains(windowIndex))
        {
            _availableWindowIndices.Add(windowIndex);
        }

        DecreaseTotalAmounts(filledWindow);
        filledWindow.Disable();

        if (_availableWindowIndices.Count == _filledHistoryWindows.Count)
        {
            _disabledHistoryText = false;
            _mainScreenView.ActivateEmptyHistoryWindow();
        }

        SaveData();
    }

    private void ProcessSettingButtonClicked()
    {
        SettingsClicked?.Invoke();
    }

    private void ProcessFullTripDataProvided(CompleteTripData completeTripData)
    {
        if (_availableWindowIndices.Count > 0)
        {
            int availableIndex = _availableWindowIndices[0];
            _availableWindowIndices.RemoveAt(0);

            var currentFilledWindow = _filledHistoryWindows[availableIndex];
            currentFilledWindow.Enable();
            currentFilledWindow.SetTotalValues(completeTripData);

            if (!_disabledHistoryText)
            {
                _mainScreenView.ChangeHistoryText();
                _disabledHistoryText = true;
            }

            if (_availableWindowIndices.Count == 0)
            {
                _mainScreenView.DisableAddTripButton();
            }
        }

        SaveData();
    }

    private void DecreaseTotalAmounts(FilledHistoryWindow currentFilledWindow)
    {
        _totalPrice -= currentFilledWindow.TotalPrice;
        _totalFuel -= currentFilledWindow.TotalFuel;

        if (_totalPrice < 0)
            _totalPrice = 0;

        if (_totalFuel < 0)
            _totalFuel = 0;

        _mainScreenView.SetTotalValues(_totalPrice, _totalFuel);
    }

    private void ProcessAddTripButtonClicked()
    {
        AddTripClicked?.Invoke();
        _mainScreenView.Disable();
    }

    private void ProcessCalculatorClicked()
    {
        CalculatorClicked?.Invoke();
        _mainScreenView.Disable();
    }

    private void SaveData()
    {
        List<FilledHistoryWindowData> windowsData = new List<FilledHistoryWindowData>();
        foreach (var window in _filledHistoryWindows)
        {
            if (!window.IsActive)
                continue;

            windowsData.Add(new FilledHistoryWindowData
            {
                PreviousTotalFuel = window.PreviousTotalFuel,
                PreviousTotalPrice = window.PreviousTotalPrice,
                CompleteTripData = window.CompleteTripData.GetCompleteSaveTripDataDto(),
                IsActive = window.IsActive
            });
        }

        _saver.SaveData(windowsData, _onboardingSeen);
    }

    private void LoadData()
    {
        var windowsData = _saver.LoadData(out _onboardingSeen);

        for (int i = 0; i < _filledHistoryWindows.Count; i++)
        {
            if (i < windowsData.Count && windowsData[i].IsActive)
            {
                _filledHistoryWindows[i].Enable();
                _filledHistoryWindows[i].SetTotalValuesFromSaveDTO(windowsData[i].CompleteTripData,
                    windowsData[i].PreviousTotalFuel, windowsData[i].PreviousTotalPrice);

                if (!_disabledHistoryText)
                {
                    _mainScreenView.ChangeHistoryText();
                    _disabledHistoryText = true;
                }

                UpdateTotalValues(_filledHistoryWindows[i]);
            }
            else
            {
                _filledHistoryWindows[i].Disable();
            }
        }

        _mainScreenView.SetTotalValues(_totalPrice, _totalFuel);
    }
}