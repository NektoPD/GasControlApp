using System;
using UnityEngine;

public class EditTripInfoPresenter : MonoBehaviour
{
    private const int DefaultIntValue = 0;

    [SerializeField] private Color _defaultTextColor;
    [SerializeField] private Color _filledTextColor;
    [SerializeField] private EditTripInfoScreenView _view;

    private int _fuelQuantity;
    private int _priceQuantity;
    private int _mileageQuantity;
    private string _date;
    private bool _canSave;

    public event Action BackButtonClicked;
    public event Action<TripData> SavedData;
    
    public bool CanSave => _canSave;

    private void Start()
    {
        _view.Disable();
    }

    private void OnEnable()
    {
        _view.FuelQuantityChanged += OnFuelQuantityChanged;
        _view.PriceQuantityChanged += OnPriceQuantityChanged;
        _view.MileageQuantityChanged += OnMileageQuantityChanged;
        _view.BackButtonClicked += OnBackButtonClicked;
        _view.DateChanged += OnDateChanged;
        _view.SaveButtonClicked += ProcessSaveButtonClicked;
    }

    private void OnDisable()
    {
        _view.FuelQuantityChanged -= OnFuelQuantityChanged;
        _view.PriceQuantityChanged -= OnPriceQuantityChanged;
        _view.MileageQuantityChanged -= OnMileageQuantityChanged;
        _view.BackButtonClicked -= OnBackButtonClicked;
        _view.DateChanged -= OnDateChanged;
        _view.SaveButtonClicked -= ProcessSaveButtonClicked;
    }

    public void ShowScreen()
    {
        _view.Enable();
    }

    public void SetViewData(TripData data)
    {
        _view.SetPriceValue(data.Price, _filledTextColor);
        _view.SetFuelValue(data.Fuel, _filledTextColor);
        _view.SetMileageValue(data.Mileage, _filledTextColor);
        _view.SetConcreteDate(data.Date);
    }

    private void OnFuelQuantityChanged(string value)
    {
        _fuelQuantity = ProcessQuantityChanged(value, _view.SetFuelValue);
    }

    private void OnPriceQuantityChanged(string value)
    {
        _priceQuantity = ProcessQuantityChanged(value, _view.SetPriceValue);
    }

    private void OnMileageQuantityChanged(string value)
    {
        _mileageQuantity = ProcessQuantityChanged(value, _view.SetMileageValue);
    }

    private void OnDateChanged(string value)
    {
        _date = value;
    }

    private void OnBackButtonClicked()
    {
        BackButtonClicked?.Invoke();
        ReturnDefaultTripDataValues();
        _view.Disable();
    }

    private void ProcessSaveButtonClicked()
    {
        TripData dataToSave = new TripData(_priceQuantity, _fuelQuantity, _mileageQuantity, _date);
        SavedData?.Invoke(dataToSave);
    }

    private int ProcessQuantityChanged(string value, Action<int, Color> setValueAction)
    {
        if (int.TryParse(value, out int intValue))
        {
            setValueAction(intValue, _filledTextColor);
            _canSave = true;
            return intValue;
        }

        setValueAction(DefaultIntValue, _defaultTextColor);
        return DefaultIntValue;
    }

    private void ReturnDefaultTripDataValues()
    {
        _view.SetFuelValue(DefaultIntValue, _defaultTextColor);
        _view.SetPriceValue(DefaultIntValue, _defaultTextColor);
        _view.SetMileageValue(DefaultIntValue, _defaultTextColor);
        _view.SetCurrentDate();
    }
}