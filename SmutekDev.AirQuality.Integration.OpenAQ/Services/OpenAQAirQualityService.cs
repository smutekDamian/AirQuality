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

    public async Task<LocalizationAirQuality> GetAirQualityForLocalization(GetAirQualityParams parameters)
    {
        GetLocationsDto dto;

        var latitude = parameters.Lat;
        var longitude = parameters.Lng;
        var localizationName = parameters.Localization;
        var distance = parameters.Distance;
        var sortOrder = parameters.SortOrder;

        if (string.IsNullOrWhiteSpace(latitude) || string.IsNullOrWhiteSpace(longitude))
        {
            dto = await _client.GetLocations(localizationName, distance: distance, sortOrder: sortOrder);
        }
        else
        {
            dto = await _client.GetLocations(latitude, longitude, distance: distance, sortOrder: sortOrder);
        }

        if (dto == null)
        {
            return null;
        }

        return new LocalizationAirQuality
        {
            LocalizationName = localizationName,
            Latitude = latitude,
            Longitude = longitude,
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