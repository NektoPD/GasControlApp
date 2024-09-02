using System;
using Bitsplash.DatePicker;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class EditTripInfoScreenView : MonoBehaviour
{
    [SerializeField] private TMP_InputField _fuelQuantity;
    [SerializeField] private TMP_InputField _price;
    [SerializeField] private TMP_InputField _currentMileage;
    [SerializeField] private TMP_Text _date;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _dateButton;
    [SerializeField] private DatePickerSettings _datePicker;
    
    private ScreenVisabilityHandler _screenVisabilityHandler;

    public event Action<string> FuelQuantityChanged;
    public event Action<string> PriceQuantityChanged;
    public event Action<string> MileageQuantityChanged;
    public event Action<string> DateChanged;
    public event Action BackButtonClicked;
    public event Action SaveButtonClicked;

    private void Awake()
    {
        _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
    }

    private void Start()
    {
        _datePicker.gameObject.SetActive(false);
        SetCurrentDate();
    }
    
    private void OnEnable()
    {
        _fuelQuantity.onValueChanged.AddListener(OnFuelQuantityChanged);
        _price.onValueChanged.AddListener(OnPriceQuantityChanged);
        _currentMileage.onValueChanged.AddListener(OnMileageQuantityChanged);
        _backButton.onClick.AddListener(OnBackButtonClicked);
        _saveButton.onClick.AddListener(OnSaveButtonClicked);
        _dateButton.onClick.AddListener(OpenCalendar);
    }

    public void SetConcreteDate(string date)
    {
        if(date == null)
            return;
        
        _date.text = date;
    }
    
    private void OpenCalendar()
    {
        _dateButton.onClick.RemoveListener(OpenCalendar);
        _dateButton.onClick.AddListener(CloseCalendar);
        _datePicker.gameObject.SetActive(true);
        _datePicker.Content.OnSelectionChanged.AddListener(SetDate);
    }

    private void CloseCalendar()
    {
        _datePicker.gameObject.SetActive(false);
        _dateButton.onClick.RemoveListener(CloseCalendar);
        _dateButton.onClick.AddListener(OpenCalendar);
    }

    private void OnDisable()
    {
        _fuelQuantity.onValueChanged.RemoveListener(OnFuelQuantityChanged);
        _price.onValueChanged.RemoveListener(OnPriceQuantityChanged);
        _currentMileage.onValueChanged.RemoveListener(OnMileageQuantityChanged);
    }
    
    public void Enable()
    {
        _screenVisabilityHandler.EnableScreen();
    }

    public void Disable()
    {
        _screenVisabilityHandler.DisableScreen();
    }

    public void SetFuelValue(int value, Color color)
    {
        _fuelQuantity.text = value.ToString();
        _fuelQuantity.textComponent.color = color;
    }

    public void SetPriceValue(int value, Color color)
    {
        _price.text = value.ToString();
        _price.textComponent.color = color;
    }
    
    public void SetMileageValue(int value, Color color)
    {
        _currentMileage.text = value.ToString();
        _currentMileage.textComponent.color = color;
    }

    private void SetDate()
    {
        string text = "";
        var selection = _datePicker.Content.Selection;
        for (int i=0; i< selection.Count; i++)
        {
            var date = selection.GetItem(i);
            text += date.ToShortDateString();
        }
        _date.text = text;
        DateChanged?.Invoke(_date.text);
    }

    public void SetCurrentDate()
    {
        _date.text = DateTime.Now.ToString("dd.MM.yyyy");
        DateChanged?.Invoke(_date.text);
    }

    
    private void OnFuelQuantityChanged(string value) => FuelQuantityChanged?.Invoke(value);
    private void OnPriceQuantityChanged(string value) => PriceQuantityChanged?.Invoke(value);
    private void OnMileageQuantityChanged(string value) => MileageQuantityChanged?.Invoke(value);
    private void OnBackButtonClicked() => BackButtonClicked?.Invoke();
    private void OnSaveButtonClicked() => SaveButtonClicked?.Invoke();
    
}
