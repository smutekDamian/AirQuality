namespace SmutekDev.AirQuality.Core.Models;

public class MeasurementStation
{
    public string Id { get; set; }
    public string City { get; set; }
    public string Name { get; set; }
    public string CountryCode { get; set; }
    public IEnumerable<QualityParameter> QualityParameters { get; set; }
}