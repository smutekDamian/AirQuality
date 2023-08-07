using SmutekDev.AirQuality.Core.Models;
using SmutekDev.AirQuality.Core.Services;
using SmutekDev.AirQuality.Integration.OpenAQ.Models.DTOs;

namespace SmutekDev.AirQuality.Integration.OpenAQ.Services;

internal class OpenAQAirQualityService : IAirQualityService
{
    private readonly OpenAQClient _client;

    public OpenAQAirQualityService(OpenAQClient client)
    {
        _client = client;
    }

    public async Task<LocalizationAirQuality> GetAirQualityForLocalization(string localizationName, string lat, string lng)
    {
        GetLocationsDto dto;

        if (string.IsNullOrWhiteSpace(lat) || string.IsNullOrWhiteSpace(lng))
        {
            dto = await _client.GetLocations(localizationName);
        }
        else
        {
            dto = await _client.GetLocations(lat, lng);
        }

        if (dto == null)
        {
            return null;
        }

        return new LocalizationAirQuality
        {
            LocalizationName = localizationName,
            Latitude = lat,
            Longitude = lng,
            MeasurementStations = ParseMeasurementsStations(dto.Results)
        };
    }

    private static IEnumerable<MeasurementStation> ParseMeasurementsStations(IEnumerable<LocationDto> locationDtos)
    {
        return locationDtos?.Select(x => new MeasurementStation
        {
            City = x.City,
            CountryCode = x.Country,
            Id = x.Id.ToString(),
            Name = x.Name,
            QualityParameters = ParseQualityParameters(x.Parameters)
        });
    }

    private static IEnumerable<QualityParameter> ParseQualityParameters(IEnumerable<ParameterDetailsDto> parametersDto)
    {
        return parametersDto?.Select(x => new QualityParameter
        {
            AverageValue = x.Average,
            LastValue = x.LastValue,
            DisplayName = x.DisplayName,
            Unit = x.Unit
        });
    }
}