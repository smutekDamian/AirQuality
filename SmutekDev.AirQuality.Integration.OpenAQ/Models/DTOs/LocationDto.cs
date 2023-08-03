namespace SmutekDev.AirQuality.Integration.OpenAQ.Models.DTOs;

internal class LocationDto
{
    public int Id { get; set; }
    public string City { get; set; }
    public string Name { get; set; }
    public object Entity { get; set; }
    public string Country { get; set; }
    public object Sources { get; set; }
    public bool IsMobile { get; set; }
    public object IsAnalysis { get; set; }
    public List<ParameterDetailsDto> Parameters { get; set; }
    public object SensorType { get; set; }
    public CoordinatesDto CoordinatesDto { get; set; }
    public DateTime LastUpdated { get; set; }
    public DateTime FirstUpdated { get; set; }
    public int Measurements { get; set; }
    public List<double> Bounds { get; set; }
}