using System;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    [SerializeField] private Color _defaultTextColor;
    [SerializeField] private Color _filledTextColor;
    [SerializeField] private CalculatorView _view;
    
    private float _fuelConsumption;
    private float _distanceTraveled;
    private float _pricePerLiter;

    public event Action BackButtonClicked;

    private void Start()
    {
        _view.DisableCalculateButton();
        _view.DisablCalculationResultPlane();
        _view.Disable();
    }

    private void OnEnable()
    {
        _view.FuelValueChanged += OnFuelConsumptionChanged;
        _view.DistanceTraveledValueChanged += OnDistanceTraveledChanged;
        _view.PricePerLiterValueChanged += OnPricePerLiterChanged;
        _view.CalculateButtonClicked += ProcessCalculation;
        _view.BackButtonClicked += ProcessBackButtonClicked;
    }

    private void OnDisable()
    {
        _view.FuelValueChanged -= OnFuelConsumptionChanged;
        _view.DistanceTraveledValueChanged -= OnDistanceTraveledChanged;
        _view.PricePerLiterValueChanged -= OnPricePerLiterChanged;
        _view.CalculateButtonClicked -= ProcessCalculation;
        _view.BackButtonClicked -= ProcessBackButtonClicked;
    }

    public void ShowScreen()
    {
        _view.Enable();
    }

    private void OnPricePerLiterChanged(string value)
    {
        _pricePerLiter = ProcessQuantityChanged(value, _view.SetPricePerLiterValue);
        UpdateCalculateButtonState();
    }
    
    private void OnDistanceTraveledChanged(string value)
    {
        _distanceTraveled = ProcessQuantityChanged(value, _view.SetDistanceTraveledValue);
        UpdateCalculateButtonState();
    }
    
    private void OnFuelConsumptionChanged(string value)
    {
        _fuelConsumption = ProcessQuantityChanged(value, _view.SetFuelConsumptionValue);
        UpdateCalculateButtonState();
    }
    
    private int ProcessQuantityChanged(string value, Action<int, Color> setValueAction)
    {
        if (int.TryParse(value, out int intValue))
        {
            setValueAction(intValue, _filledTextColor);
            return intValue;
        }

        setValueAction(default, _defaultTextColor);
        return default;
    }
    
    private void UpdateCalculateButtonState()
    {
        if (_fuelConsumption > 0 && _distanceTraveled > 0 && _pricePerLiter > 0)
        {
            _view.EnableCalculateButton();
        }
        else
        {
            _view.DisableCalculateButton();
        }
    }

    private void ProcessCalculation()
    {
        _view.EnableCalculationResultPlane();
        _view.SetCalculationResults(CalculateFuelQuantity(), CalculatePrice());
    }

    private float CalculateFuelQuantity()
    {
        if (_fuelConsumption < 0 && _distanceTraveled < 0 && _pricePerLiter < 0)
            return default;

        return _fuelConsumption * _distanceTraveled / 100;
    }

    private float CalculatePrice()
    {
        if (_fuelConsumption < 0 && _distanceTraveled < 0 && _pricePerLiter < 0)
            return default;

        return (_fuelConsumption * _distanceTraveled / 100) * _pricePerLiter;
    }

    private void ProcessBackButtonClicked()
    {
        _view.DisableCalculateButton();
        _view.SetPricePerLiterValue(default, _defaultTextColor);
        _view.SetDistanceTraveledValue(default, _defaultTextColor);
        _view.SetFuelConsumptionValue(default, _defaultTextColor);
        _view.DisablCalculationResultPlane();
        BackButtonClicked?.Invoke();
        _view.Disable();
    }
}
