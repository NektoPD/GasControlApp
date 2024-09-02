using System;

public class CompleteTripData
{
    private TripData _startTripData;
    private TripData _endTripData;
    private string _tripName;

    public CompleteTripData(TripData startTripData, string tripName, TripData endTripData)
    {
        _startTripData = startTripData;
        _tripName = tripName;
        _endTripData = endTripData;
    }

    public string TripName => _tripName;

    public TripData EndTripData => _endTripData;

    public TripData StartTripData => _startTripData;

    public void SetNewStartTripData(TripData newStartTripData)
    {
        if (newStartTripData == null)
            return;

        _startTripData = newStartTripData;
    }
    
    public CompleteTripDataDTO GetCompleteSaveTripDataDto()
    {
        return new CompleteTripDataDTO(_startTripData.SaveDataDto, _endTripData.SaveDataDto, _tripName);
    }

    public void SetNewEndTripData(TripData newEndTripData)
    {
        if (newEndTripData == null)
            return;

        _endTripData = newEndTripData;
    }

    public void SetNewTripName(string name)
    {
        if (name == null)
            return;

        _tripName = name;
    }
}

[Serializable]
public class CompleteTripDataDTO
{
    public TripDataDTO StartTripData;
    public TripDataDTO EndTripData;
    public string TripName;

    public CompleteTripDataDTO(TripDataDTO startTripData, TripDataDTO endTripData, string tripName)
    {
        StartTripData = startTripData;
        EndTripData = endTripData;
        TripName = tripName;
    }
}
