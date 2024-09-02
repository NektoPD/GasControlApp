using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditFilledTripInfoScreen : MonoBehaviour
{
    [SerializeField] private EditFilledTripInfoScreenView _view;
    [SerializeField] private EditTripInfoPresenter _startOfTripScreen;
    [SerializeField] private EditTripInfoPresenter _endOfTripScreen;

    private CompleteTripData _currentTripData;
    
    public event Action BackButtonClicked;
    public event Action EditStartTripClicked;
    public event Action EditEndTripClicked;
    public event Action<CompleteTripData> TripDataUpdated;
    
    private void Start()
    {
        _view.Disable();
    }

    private void OnEnable()
    {
        _view.BackButtonClicked += ProcessBackButtonClicked;
        _view.EditStartTripButtonClicked += ProcessEditStartTripClicked;
        _view.EditEndTripButtonClicked += ProcessEditEndTripClicked;
        _startOfTripScreen.SavedData += SaveNewStartTripData;
        _endOfTripScreen.SavedData += SaveNewEndTripData;
        _view.TripNameChanged += SaveNewTripName;
    }

    private void OnDisable()
    {
        _view.BackButtonClicked -= ProcessBackButtonClicked;
        _view.EditStartTripButtonClicked -= ProcessEditStartTripClicked;
        _view.EditEndTripButtonClicked -= ProcessEditEndTripClicked;
        _startOfTripScreen.SavedData -= SaveNewStartTripData;
        _endOfTripScreen.SavedData -= SaveNewEndTripData;
        _view.TripNameChanged -= SaveNewTripName;
    }
    
    private void SaveNewEndTripData(TripData newEndTripData)
    {
        if(newEndTripData == null && _currentTripData == null)
            throw new ArgumentNullException();
        
        _currentTripData.SetNewEndTripData(newEndTripData);
        _view.SetData(_currentTripData);
        TripDataUpdated?.Invoke(_currentTripData);
    }

    private void SaveNewStartTripData(TripData newStartTripData)
    {
        if(newStartTripData == null && _currentTripData == null)
            throw new ArgumentNullException();
        
        _currentTripData.SetNewStartTripData(newStartTripData);
        _view.SetData(_currentTripData);
        TripDataUpdated?.Invoke(_currentTripData);
    }

    private void SaveNewTripName(string newTripName)
    {
        if(newTripName == null && _currentTripData == null)
            throw new ArgumentNullException();
        
        _currentTripData.SetNewTripName(newTripName);
        _view.SetData(_currentTripData);
        TripDataUpdated?.Invoke(_currentTripData);
    }

    private void ProcessEditEndTripClicked()
    {
        if(_currentTripData == null)
            return;
        
        EditEndTripClicked?.Invoke();
        _endOfTripScreen.SetViewData(_currentTripData.EndTripData);
    }

    private void ProcessEditStartTripClicked()
    {
        if(_currentTripData == null)
            return;
        
        EditStartTripClicked?.Invoke();
        _startOfTripScreen.SetViewData(_currentTripData.StartTripData);
    }

    private void ProcessBackButtonClicked()
    {
        BackButtonClicked?.Invoke();
        Disable();
    }

    public void ShowScreen()
    {
        _view.Enable();
    }

    private void Disable()
    {
        _view.Disable();
    }

    public void InitializeScreen(CompleteTripData data)
    {
        if(_view.ScreenEnabled)
            return;
        
        ShowScreen();
        _currentTripData = data;
        _view.SetData(_currentTripData);
    }
    
}
