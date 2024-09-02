using System;

public class TripData
{
    private int _price;
    private int _fuel;
    private int _mileage;
    private string _date;

    public TripDataDTO SaveDataDto { get; private set; }
    
    public TripData(int price, int fuel, int mileage, string date)
    {
        _price = price;
        _fuel = fuel;
        _mileage = mileage;
        _date = date;

        SaveDataDto = new TripDataDTO(_price, _fuel, _mileage, _date);
    }

    public string Date => _date;

    public int Mileage => _mileage;

    public int Fuel => _fuel;

    public int Price => _price;
}

[Serializable]
public class TripDataDTO
{
    public int PriceToSave;
    public int FuelToSave;
    public int MileageToSave;
    public string DateToSave;

    public TripDataDTO(int priceToSave, int fuelToSave, int mileageToSave, string dateToSave)
    {
        PriceToSave = priceToSave;
        FuelToSave = fuelToSave;
        MileageToSave = mileageToSave;
        DateToSave = dateToSave;
    }
}