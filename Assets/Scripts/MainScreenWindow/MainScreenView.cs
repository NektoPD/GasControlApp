using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class MainScreenView : MonoBehaviour
{
    [SerializeField] private TMP_Text _totalPrice;
    [SerializeField] private TMP_Text _totalFuel;
    [SerializeField] private Button _addTripButton;
    [SerializeField] private Button _calculatorButton;
    [SerializeField] private TMP_Text _historyText;
    [SerializeField] private TMP_Text _tripHistoryText;
    [SerializeField] private GameObject _historyPlane;
    [SerializeField] private Button _settingsButton;
    
    private ScreenVisabilityHandler _screenVisabilityHandler;
    
    public event Action AddTripButtonClicked;
    public event Action CalculatorButtonClicked;
    public event Action SettingsButtonClicked;
    
    private void Awake()
    {
        _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
    }
    
    private void Start()
    {
        _addTripButton.onClick.AddListener(ProcessAddTripButtonClicked);
        _calculatorButton.onClick.AddListener(ProcessCalculatorButtonClicked);
        _settingsButton.onClick.AddListener(ProcessSettingsButtonCLicked);
        _tripHistoryText.gameObject.SetActive(false);
    }

    private void ProcessSettingsButtonCLicked()
    {
        SettingsButtonClicked?.Invoke();
    }

    private void OnDisable()
    {
        _addTripButton.onClick.RemoveListener(ProcessAddTripButtonClicked);
        _calculatorButton.onClick.RemoveListener(ProcessCalculatorButtonClicked);
    }

    public void Enable()
    {
        _screenVisabilityHandler.EnableScreen();
    }

    public void Disable()
    {
        _screenVisabilityHandler.DisableScreen();
    }

    public void ChangeHistoryText()
    {
        _historyText.gameObject.SetActive(false);
        _tripHistoryText.gameObject.SetActive(true);
        _historyPlane.gameObject.SetActive(false);
    }

    public void ActivateEmptyHistoryWindow()
    {
        _historyText.gameObject.SetActive(true);
        _tripHistoryText.gameObject.SetActive(false);
        _historyPlane.gameObject.SetActive(true);
    }

    public void DisableAddTripButton()
    {
        _addTripButton.interactable = false;
    }

    public void SetTotalValues(int totalPrice, int totalFuel)
    {
        _totalPrice.text = "$" + totalPrice;
        _totalFuel.text = totalFuel.ToString();
    }

    private void ProcessCalculatorButtonClicked()
    {
        CalculatorButtonClicked?.Invoke();
    }

    private void ProcessAddTripButtonClicked()
    {
        AddTripButtonClicked?.Invoke();
    }
}
