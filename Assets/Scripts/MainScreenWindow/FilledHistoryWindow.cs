using System;
using UnityEngine;

public class FilledHistoryWindow : MonoBehaviour
{
    [SerializeField] private FilledHistoryWindowView _view;

    private string _name;
    private int _totalFuel;
    private int _totalPrice;
    private bool _isActive;

    private CompleteTripData _completeTripData;

    public int PreviousTotalFuel { get; private set; }
    public int PreviousTotalPrice { get; private set; }

    public event Action<FilledHistoryWindow> Deleted;
    public event Action<FilledHistoryWindow> SeeMorePressed;
    public event Action<FilledHistoryWindow> TripDataUpdated;
    
    public bool IsActive => _isActive;
    public int TotalFuel => _totalFuel;

    public int TotalPrice => _totalPrice;

    public CompleteTripData CompleteTripData => _completeTripData;

    private void OnEnable()
    {
        _view.DeleteButtonPressed += ProcessDelete;
        _view.SeeMoreButtonPressed += ProcessSeeMore;
    }

    private void OnDisable()
    {
        _view.DeleteButtonPressed -= ProcessDelete;
        _view.SeeMoreButtonPressed -= ProcessSeeMore;
    }

    public void SetTotalValues(CompleteTripData completeTripData)
    {
        if (completeTripData == null)
            return;

        PreviousTotalFuel = _totalFuel;
        PreviousTotalPrice = _totalPrice;
        
        _completeTripData = completeTripData;

        _totalFuel = _completeTripData.StartTripData.Fuel + _completeTripData.EndTripData.Fuel;
        _totalPrice = _completeTripData.StartTripData.Price + _completeTripData.EndTripData.Price;
        _name = _completeTripData.TripName;
        _view.SetValues(_name, _totalFuel, _totalPrice);
        TripDataUpdated?.Invoke(this);
    }

    public void SetTotalValuesFromSaveDTO(CompleteTripDataDTO savedtripData, int previousTotalFuel,
        int previousTotalPrice)
    {
        PreviousTotalFuel = previousTotalFuel;
        PreviousTotalPrice = previousTotalPrice;
        _completeTripData = new CompleteTripData(new TripData(savedtripData.StartTripData.PriceToSave,
                savedtripData.StartTripData.FuelToSave,
                savedtripData.StartTripData.MileageToSave, savedtripData.StartTripData.DateToSave),
            savedtripData.TripName,
            new TripData(savedtripData.EndTripData.PriceToSave, savedtripData.EndTripData.FuelToSave,
                savedtripData.EndTripData.MileageToSave, savedtripData.EndTripData.DateToSave));

        _totalFuel = _completeTripData.StartTripData.Fuel + _completeTripData.EndTripData.Fuel;
        _totalPrice = _completeTripData.StartTripData.Price + _completeTripData.EndTripData.Price;
        _name = _completeTripData.TripName;
        _view.SetValues(_name, _totalFuel, _totalPrice);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        _isActive = true;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        _isActive = false;
    }

    private void ProcessDelete()
    {
        Deleted?.Invoke(this);
    }

    private void ProcessSeeMore()
    {
        SeeMorePressed?.Invoke(this);
    }
}

[Serializable]
public class FilledHistoryWindowData
{
    public int PreviousTotalFuel;
    public int PreviousTotalPrice;
    public CompleteTripDataDTO CompleteTripData;
    public bool IsActive;
}