namespace SmutekDev.AirQuality.Core.Models;

public class LocalizationAirQuality
{
    public string LocalizationName { get; set; }
    public string Longitude { get; set; }
    public string Latitude { get; set; }
    public IEnumerable<MeasurementStation> MeasurementStations { get; set; }
}