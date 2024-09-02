using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FilledHistoryWindowView : MonoBehaviour
{
    [SerializeField] private TMP_Text _tripName;
    [SerializeField] private TMP_Text _totalFuelQuantity;
    [SerializeField] private TMP_Text _totalPriceQuantity;
    [SerializeField] private Button _seeMoreButton;
    [SerializeField] private Button _deleteButton;

    public event Action DeleteButtonPressed;
    public event Action SeeMoreButtonPressed;
    
    private void OnEnable()
    {
        _deleteButton.onClick.AddListener(ProcessDeleteButtonPressed);
        _seeMoreButton.onClick.AddListener(ProcessSeeMoreButtonPressed);
    }

    private void ProcessSeeMoreButtonPressed()
    {
        SeeMoreButtonPressed?.Invoke();
    }

    private void ProcessDeleteButtonPressed()
    {
        DeleteButtonPressed?.Invoke();
    }

    public void SetValues(string tripName, int totalFuel, int totalPrice)
    {
        _tripName.text = tripName;
        _totalFuelQuantity.text = totalFuel.ToString();
        _totalPriceQuantity.text = totalPrice.ToString();
    }
}
