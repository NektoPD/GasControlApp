using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTripScreen : MonoBehaviour
{
    [SerializeField] private OpenTripScreenView _view;
    [SerializeField] private EditFilledTripInfoScreen _editFilledTripInfoScreen;
    
    private int _totalPrice;
    private int _totalFuel;
    private CompleteTripData _completeTripData;
    private FilledHistoryWindow _currentFilledWindow;

    public event Action BackButtonClicked;
    public event Action<CompleteTripData> EditButtonClicked;

    private void Start()
    {
        _view.Disable();
    }

    private void OnEnable()
    {
        _view.BackButtonClicked += ProcessBackButtonClicked;
        _view.EditButtonClicked += ProcessEditButtonClicked;
        _editFilledTripInfoScreen.TripDataUpdated += SaveNewCompleteData;
    }

    private void OnDisable()
    {
        _view.BackButtonClicked -= ProcessBackButtonClicked;
        _view.EditButtonClicked -= ProcessEditButtonClicked;
        _editFilledTripInfoScreen.TripDataUpdated -= SaveNewCompleteData;
    }

    private void SaveNewCompleteData(CompleteTripData newCompleteData)
    {
        if(newCompleteData == null)
            return;

        _completeTripData = newCompleteData;
        SetCompleteData();
        UpdateFilledWindowValues();
    }

    private void ProcessEditButtonClicked()
    {
        if(_completeTripData == null)
            return;
        
        EditButtonClicked?.Invoke(_completeTripData);
    }

    private void ProcessBackButtonClicked()
    {
        BackButtonClicked?.Invoke();
        _view.Disable();
    }

    public void ShowScreen()
    {
        _view.Enable();
    }
    
    public void InitializeWindow(FilledHistoryWindow filledHistoryWindow)
    {
        if(_view.ScreenEnabled)
            return;
        
        ShowScreen();
        _currentFilledWindow = filledHistoryWindow;
        _completeTripData = filledHistoryWindow.CompleteTripData;
        
        SetCompleteData();
    }

    private void SetCompleteData()
    {
        _totalPrice = _completeTripData.StartTripData.Price + _completeTripData.EndTripData.Price;
        _totalFuel = _completeTripData.StartTripData.Fuel + _completeTripData.EndTripData.Fuel;
        
        _view.SetTripValues(_completeTripData, _totalPrice, _totalFuel);
    }

    private void UpdateFilledWindowValues()
    {
        _currentFilledWindow.SetTotalValues(_completeTripData);
    }
}
