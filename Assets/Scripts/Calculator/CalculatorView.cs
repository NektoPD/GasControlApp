using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class CalculatorView : MonoBehaviour
{
    [SerializeField] private TMP_InputField _fuelConsumption;
    [SerializeField] private TMP_InputField _distanceTraveled;
    [SerializeField] private TMP_InputField _pricePerLiter;
    [SerializeField] private TMP_Text _resultFuel;
    [SerializeField] private TMP_Text _resultPrice;
    [SerializeField] private Button _calculateButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private GameObject _calculationResultPlane;
    
    private ScreenVisabilityHandler _screenVisabilityHandler;

    public event Action<string> FuelValueChanged;
    public event Action<string> DistanceTraveledValueChanged;
    public event Action<string> PricePerLiterValueChanged;
    public event Action CalculateButtonClicked;
    public event Action BackButtonClicked;

    private void Awake()
    {
        _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
    }
    
    private void OnEnable()
    {
        _fuelConsumption.onValueChanged.AddListener(ProcessFuelConsumptionValueChanged);
        _distanceTraveled.onValueChanged.AddListener(ProcessDistanceTraveledValueChanged);
        _pricePerLiter.onValueChanged.AddListener(ProcessPricePerLiterValueChanged);
        _calculateButton.onClick.AddListener(ProcessCalculateButtonClicked);
        _backButton.onClick.AddListener(ProcessBackButtonClicked);
    }

    private void OnDisable()
    {
        _fuelConsumption.onValueChanged.RemoveListener(ProcessFuelConsumptionValueChanged);
        _distanceTraveled.onValueChanged.RemoveListener(ProcessDistanceTraveledValueChanged);
        _pricePerLiter.onValueChanged.RemoveListener(ProcessPricePerLiterValueChanged);
        _calculateButton.onClick.RemoveListener(ProcessCalculateButtonClicked);
        _backButton.onClick.RemoveListener(ProcessBackButtonClicked);
    }
    
    public void Enable()
    {
        _screenVisabilityHandler.EnableScreen();
    }

    public void Disable()
    {
        _screenVisabilityHandler.DisableScreen();
    }

    public void EnableCalculateButton()
    {
        _calculateButton.interactable = true;
    }

    public void DisableCalculateButton()
    {
        _calculateButton.interactable = false;
    }

    public void SetFuelConsumptionValue(int value, Color color)
    {
        _fuelConsumption.text = value.ToString(); 
        _fuelConsumption.textComponent.color = color;
    }
    
    public void SetDistanceTraveledValue(int value, Color color)
    {
        _distanceTraveled.text = value.ToString(); 
        _distanceTraveled.textComponent.color = color;
    }
    
    public void SetPricePerLiterValue(int value, Color color)
    {
        _pricePerLiter.text = value.ToString(); 
        _pricePerLiter.textComponent.color = color;
    }

    public void EnableCalculationResultPlane()
    {
        if(_calculationResultPlane.activeSelf)
            return;
        
        _calculationResultPlane.gameObject.SetActive(true);
    }

    public void DisablCalculationResultPlane()
    {
        _calculationResultPlane.gameObject.SetActive(false);
    }

    public void SetCalculationResults(float fuelQuantity, float price)
    {
        _resultFuel.text = fuelQuantity.ToString();
        _resultPrice.text = price.ToString();
    }

    private void ProcessBackButtonClicked()
    {
        BackButtonClicked?.Invoke();
    }

    private void ProcessCalculateButtonClicked()
    {
        CalculateButtonClicked?.Invoke();
    }

    private void ProcessPricePerLiterValueChanged(string value)
    {
        PricePerLiterValueChanged?.Invoke(value);
    }

    private void ProcessDistanceTraveledValueChanged(string value)
    {
        DistanceTraveledValueChanged?.Invoke(value);
    }

    private void ProcessFuelConsumptionValueChanged(string value)
    {
        FuelValueChanged?.Invoke(value);
    }
}
