namespace SmutekDev.AirQuality.Core.Models;

public class QualityParameter
{
    public string Unit { get; set; }
    public double AverageValue { get; set; }
    public double LastValue { get; set; }
    public string DisplayName { get; set; }
}