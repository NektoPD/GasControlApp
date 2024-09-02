using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class OpenTripScreenView : MonoBehaviour
{
    [SerializeField] private TMP_Text _tripName;
    [SerializeField] private TMP_Text _totalPrice;
    [SerializeField] private TMP_Text _totalFuel;

    [SerializeField] private TMP_Text _startTripFuel;
    [SerializeField] private TMP_Text _startTripPrice;
    [SerializeField] private TMP_Text _startTripMileage;
    [SerializeField] private TMP_Text _startTripDate;
    
    [SerializeField] private TMP_Text _endTripFuel;
    [SerializeField] private TMP_Text _endTripPrice;
    [SerializeField] private TMP_Text _endTripMileage;
    [SerializeField] private TMP_Text _endTripDate;

    [SerializeField] private Button _backButton;
    [SerializeField] private Button _editButton;
    
    private ScreenVisabilityHandler _screenVisabilityHandler;
    private bool _screenEnabled;

    public event Action BackButtonClicked;
    public event Action EditButtonClicked;

    public bool ScreenEnabled => _screenEnabled;

    private void Awake()
    {
        _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
    }

    private void OnEnable()
    {
        _backButton.onClick.AddListener(ProcessBackButtonClicked);
        _editButton.onClick.AddListener(ProcessEditButtonClicked);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(ProcessBackButtonClicked);
        _editButton.onClick.RemoveListener(ProcessEditButtonClicked);
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

    private void ProcessEditButtonClicked()
    {
        EditButtonClicked?.Invoke();
    }

    private void ProcessBackButtonClicked()
    {
        BackButtonClicked?.Invoke();
    }

    public void SetTripValues(CompleteTripData tripData, int totalPrice, int totalFuel)
    {
        _tripName.text = tripData.TripName;
        _totalPrice.text = totalPrice.ToString();
        _totalFuel.text = totalFuel.ToString();

        _startTripFuel.text = tripData.StartTripData.Fuel.ToString();
        _startTripPrice.text = tripData.StartTripData.Price.ToString();
        _startTripMileage.text = tripData.StartTripData.Mileage.ToString();
        _startTripDate.text = tripData.StartTripData.Date;

        _endTripFuel.text = tripData.EndTripData.Fuel.ToString();
        _endTripPrice.text = tripData.EndTripData.Price.ToString();
        _endTripMileage.text = tripData.EndTripData.Mileage.ToString();
        _endTripDate.text = tripData.EndTripData.Date;
    }
}
