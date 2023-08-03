namespace SmutekDev.AirQuality.Integration.OpenAQ.Models.DTOs;

internal class ParameterDetailsDto
{
    public int Id { get; set; }
    public string Unit { get; set; }
    public int Count { get; set; }
    public double Average { get; set; }
    public double LastValue { get; set; }
    public string Parameter { get; set; }
    public string DisplayName { get; set; }
    public DateTime LastUpdated { get; set; }
    public int ParameterId { get; set; }
    public DateTime FirstUpdated { get; set; }
    public object Manufacturers { get; set; }
}