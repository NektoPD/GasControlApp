using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class EditFilledTripInfoScreenView : MonoBehaviour
{
    [SerializeField] private TMP_Text _startTripfuelQuantityValue;
    [SerializeField] private TMP_Text _startTripPriceValue;
    [SerializeField] private TMP_Text _startTripCurrentMileageValue;
    [SerializeField] private TMP_Text _startTripCurrentDate;

    [SerializeField] private TMP_Text _endTripfuelQuantityValue;
    [SerializeField] private TMP_Text _endTripPriceValue;
    [SerializeField] private TMP_Text _endTripCurrentMileageValue;
    [SerializeField] private TMP_Text _endTripCurrentDate;

    [SerializeField] private Button _backButton;
    [SerializeField] private Button _editButton;
    [SerializeField] private Button _editStartTripButton;
    [SerializeField] private Button _editEndTripButton;

    [SerializeField] private TMP_Text _titleTripName;
    [SerializeField] private TMP_InputField _tripName;
    
    private ScreenVisabilityHandler _screenVisabilityHandler;
    private bool _screenEnabled;

    public event Action EditButtonClicked;
    public event Action BackButtonClicked;
    public event Action EditStartTripButtonClicked;
    public event Action EditEndTripButtonClicked;
    public event Action<string> TripNameChanged;
    
    public bool ScreenEnabled => _screenEnabled;
    
    private void Awake()
    {
        _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
    }

    private void OnEnable()
    {
        _editButton.onClick.AddListener(ProcessEditButtonClicked);
        _backButton.onClick.AddListener(ProcessBackButtonClicked);
        _editStartTripButton.onClick.AddListener(ProcessEditStartTripButtonClicked);
        _editEndTripButton.onClick.AddListener(ProcessEditEndTripButtonClicked);
        _tripName.onValueChanged.AddListener(ProcessTripNameChanged);
    }

    private void ProcessTripNameChanged(string newTripName)
    {
        TripNameChanged?.Invoke(newTripName);
    }

    private void ProcessEditEndTripButtonClicked()
    {
        EditEndTripButtonClicked?.Invoke();
    }

    private void ProcessEditStartTripButtonClicked()
    {
        EditStartTripButtonClicked?.Invoke();
    }

    private void ProcessBackButtonClicked()
    {
        BackButtonClicked?.Invoke();
    }

    private void ProcessEditButtonClicked()
    {
        EditButtonClicked?.Invoke();
    }

    public void Enable()
    {
        _screenVisabilityHandler.EnableScreen();
        _screenEnabled = true;
    }

    public void Disable()
    {
        _screenVisabilityHandler.DisableScreen();
        _screenEnabled = false;
    }

    public void SetData(CompleteTripData tripData)
    {
        _startTripfuelQuantityValue.text = tripData.StartTripData.Fuel.ToString();
        _startTripPriceValue.text = tripData.StartTripData.Price.ToString();
        _startTripCurrentMileageValue.text = tripData.StartTripData.Mileage.ToString();
        _startTripCurrentDate.text = tripData.StartTripData.Date;

        _endTripfuelQuantityValue.text = tripData.EndTripData.Fuel.ToString();
        _endTripPriceValue.text = tripData.EndTripData.Price.ToString();
        _endTripCurrentMileageValue.text = tripData.EndTripData.Mileage.ToString();
        _endTripCurrentDate.text = tripData.EndTripData.Date;

        _tripName.text = tripData.TripName;
        _titleTripName.text = tripData.TripName;
    }
    
}
