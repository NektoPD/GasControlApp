using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class AddTripScreenView : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _addStartTripButton;
    [SerializeField] private Button _addEndTripButton;

    [SerializeField] private Button _resetStartTripButton;
    [SerializeField] private Button _resetEndTripButton;

    [SerializeField] private TMP_Text _startTripfuelQuantityValue;
    [SerializeField] private TMP_Text _startTripPriceValue;
    [SerializeField] private TMP_Text _startTripCurrentMileageValue;
    [SerializeField] private TMP_Text _startTripCurrentDate;

    [SerializeField] private TMP_Text _endTripfuelQuantityValue;
    [SerializeField] private TMP_Text _endTripPriceValue;
    [SerializeField] private TMP_Text _endTripCurrentMileageValue;
    [SerializeField] private TMP_Text _endTripCurrentDate;

    [SerializeField] private TMP_InputField _tripName;

    private ScreenVisabilityHandler _screenVisabilityHandler;

    public event Action AddStartTripButtonClicked;
    public event Action AddEndOfTripButtonClicked;
    public event Action ResetStartTripButtonClicked;
    public event Action ResetEndTripButtonClicked;
    public event Action BackButtonClicked;
    public event Action SaveButtonClicked;
    public event Action<string> TripNameInputed;

    private void Awake()
    {
        _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
    }

    private void Start()
    {
        _resetStartTripButton.gameObject.SetActive(false);
        _resetEndTripButton.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _backButton.onClick.AddListener(ProcessBackButtonClicked);
        _addStartTripButton.onClick.AddListener(ProcessAddStartTripButtonClicked);
        _addEndTripButton.onClick.AddListener(ProcessEndTripButtonClicked);
        _tripName.onValueChanged.AddListener(ProcessTripNameChanged);
        _saveButton.onClick.AddListener(OnSaveButtonClicked);
    }

    private void OnSaveButtonClicked()
    {
        SaveButtonClicked?.Invoke();
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(ProcessBackButtonClicked);
        _addStartTripButton.onClick.RemoveListener(ProcessAddStartTripButtonClicked);
        _addEndTripButton.onClick.RemoveListener(ProcessEndTripButtonClicked);
        _tripName.onValueChanged.RemoveListener(ProcessTripNameChanged);
    }

    private void ProcessTripNameChanged(string input)
    {
        TripNameInputed?.Invoke(input);
    }

    public void SetTripName(string tripName, Color color)
    {
        _tripName.text = tripName;
        _tripName.textComponent.color = color;
    }

    public void Enable()
    {
        _screenVisabilityHandler.EnableScreen();
    }

    public void Disable()
    {
        _screenVisabilityHandler.DisableScreen();
    }

    private void ProcessBackButtonClicked()
    {
        BackButtonClicked?.Invoke();
    }

    private void ProcessAddStartTripButtonClicked()
    {
        AddStartTripButtonClicked?.Invoke();
    }

    private void ProcessEndTripButtonClicked()
    {
        AddEndOfTripButtonClicked?.Invoke();
    }

    private void ProccessResetStartTripButtonClicked()
    {
        ResetStartTripButtonClicked?.Invoke();
    }
    
    private void ProccessResetEndTripButtonClicked()
    {
        ResetEndTripButtonClicked?.Invoke();
    }

    public void SetStartTripValues(int price, int fuel, int mileage, string date)
    {
        _startTripPriceValue.text = price.ToString();
        _startTripfuelQuantityValue.text = fuel.ToString();
        _startTripCurrentMileageValue.text = mileage.ToString();
        _startTripCurrentDate.text = date;
    }

    public void SetEndTripValues(int price, int fuel, int mileage, string date)
    {
        _endTripPriceValue.text = price.ToString();
        _endTripfuelQuantityValue.text = fuel.ToString();
        _endTripCurrentMileageValue.text = mileage.ToString();
        _endTripCurrentDate.text = date;
    }

    public void ActivateStartTripResetButton()
    {
        _addStartTripButton.onClick.RemoveListener(ProcessAddStartTripButtonClicked);
        _addStartTripButton.gameObject.SetActive(false);

        _resetStartTripButton.gameObject.SetActive(true);
        _resetStartTripButton.onClick.AddListener(ProccessResetStartTripButtonClicked);
    }

    public void ActivateAddStartTripButton()
    {
        _resetStartTripButton.onClick.RemoveListener(ProccessResetStartTripButtonClicked);
        _resetStartTripButton.gameObject.SetActive(false);

        _addStartTripButton.gameObject.SetActive(true);
        _addStartTripButton.onClick.AddListener(ProcessAddStartTripButtonClicked);
    }

    public void ActivateEndTripResetButton()
    {
        _addEndTripButton.onClick.RemoveListener(ProcessEndTripButtonClicked);
        _addEndTripButton.gameObject.SetActive(false);

        _resetEndTripButton.gameObject.SetActive(true);
        _resetEndTripButton.onClick.AddListener(ProccessResetEndTripButtonClicked);
    }

    public void ActivateAddEndTripButton()
    {
        _resetEndTripButton.onClick.RemoveListener(ProccessResetEndTripButtonClicked);
        _resetEndTripButton.gameObject.SetActive(false);

        _addEndTripButton.gameObject.SetActive(true);
        _addEndTripButton.onClick.AddListener(ProcessEndTripButtonClicked);
    }
}