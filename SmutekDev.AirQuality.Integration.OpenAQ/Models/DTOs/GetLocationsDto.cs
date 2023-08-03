namespace SmutekDev.AirQuality.Integration.OpenAQ.Models.DTOs;

internal class GetLocationsDto
{
    public MetaDto Meta { get; set; }
    public List<LocationDto> Results { get; set; }
}