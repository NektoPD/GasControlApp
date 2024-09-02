using System;
using UnityEngine;

public class AddTripScreenPresenter : MonoBehaviour
{
    private const int DefaultFuelData = 0;
    private const int DefautPriceData = 0;
    private const int DefaultMileageData = 0;
    private const string DefaultDateData = "00.00.0000";
    
    [SerializeField] private AddTripScreenView _view;
    [SerializeField] private EditTripInfoPresenter _addStartTripScreen;
    [SerializeField] private EditTripInfoPresenter _addEndTripScreen;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _filledTextColor;

    //private CompleteTripData _completeTripData;
    private TripData _currentStartTripData;
    private TripData _currentEndTripData;
    private string _currentTripName;
    
    public event Action BackToMenuButtonClicked;
    public event Action AddStartTripClicked;
    public event Action AddEndTripClicked;
    public event Action<CompleteTripData> FullTripDataSaved;

    private void Start()
    {
        _view.Disable();
        SetStartTripDefaultVaules();
        SetEndTripDefaultVaules();
    }

    private void OnEnable()
    {
        _view.BackButtonClicked += ProcessBackButtonClicked;
        
        _view.AddStartTripButtonClicked += ProcessAddStartTripClicked;
        _view.ResetStartTripButtonClicked += ResetStartTripData;
        
        _view.AddEndOfTripButtonClicked += ProcessAddEndTripClicked;
        _view.ResetEndTripButtonClicked += ResetEndTripData;
        
        _addStartTripScreen.SavedData += ProcesStartTripNewDataSaved;
        _addEndTripScreen.SavedData += ProcesEndTripNewDataSaved;

        _view.TripNameInputed += ChangeTripName;

        _view.SaveButtonClicked += SaveData;
    }

    private void SaveData()
    {
        if(_currentStartTripData == null && _currentEndTripData == null && _currentTripName == null)
            return;

        var dataToSave = new CompleteTripData(_currentStartTripData, _currentTripName, _currentEndTripData);
        FullTripDataSaved?.Invoke(dataToSave);
        ResetStartTripData();
        ResetEndTripData();
    }

    private void ChangeTripName(string tripName)
    {
        _currentTripName = tripName;
        _view.SetTripName(tripName, _filledTextColor);
    }

    private void ResetStartTripData()
    {
        SetStartTripDefaultVaules();
        _view.ActivateAddStartTripButton();
        _currentStartTripData = null;
    }

    private void ResetEndTripData()
    {
        SetEndTripDefaultVaules();
        _view.ActivateAddEndTripButton();
        _currentEndTripData = null;
    }

    private void ProcesStartTripNewDataSaved(TripData dataToProcess)
    {
        _currentStartTripData = dataToProcess;
        _view.SetStartTripValues(dataToProcess.Price, dataToProcess.Fuel, dataToProcess.Mileage, dataToProcess.Date);
        _view.ActivateStartTripResetButton();
    }
    
    private void ProcesEndTripNewDataSaved(TripData dataToProcess)
    {
        _currentEndTripData = dataToProcess;
        _view.SetEndTripValues(dataToProcess.Price, dataToProcess.Fuel, dataToProcess.Mileage, dataToProcess.Date);
        _view.ActivateEndTripResetButton();
    }

    private void OnDisable()
    {
        _view.BackButtonClicked -= ProcessBackButtonClicked;
        _view.AddStartTripButtonClicked -= ProcessAddStartTripClicked;
    }

    public void ShowScreen()
    {
        _view.Enable();
    }

    private void ProcessBackButtonClicked()
    {
        BackToMenuButtonClicked?.Invoke();
        _view.Disable();
    }

    private void ProcessAddStartTripClicked()
    {
        AddStartTripClicked?.Invoke();
        _view.Disable();
    }

    private void ProcessAddEndTripClicked()
    {
        AddEndTripClicked?.Invoke();
        _view.Disable();
    }

    private void SetStartTripDefaultVaules()
    {
        _view.SetStartTripValues(DefautPriceData, DefaultFuelData, DefaultMileageData, DefaultDateData);
    }
    
    private void SetEndTripDefaultVaules()
    {
        _view.SetEndTripValues(DefautPriceData, DefaultFuelData, DefaultMileageData, DefaultDateData);
    }
}
