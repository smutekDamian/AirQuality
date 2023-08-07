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
        IEnumerable<LocationDto> locations;

        var latitude = parameters.Lat;
        var longitude = parameters.Lng;
        var localizationName = parameters.Localization;
        var distance = parameters.Distance;
        var sortOrder = parameters.SortOrder;
        var pageSize = parameters.PageSize;

        if (string.IsNullOrWhiteSpace(latitude) || string.IsNullOrWhiteSpace(longitude))
        {
            locations = await GetAllResults(page => 
                _client.GetLocations(
                    localizationName, 
                    page, 
                    pageSize, 
                    (page - 1) * pageSize, 
                    distance, 
                    sortOrder));
        }
        else
        {
            locations = await GetAllResults(page =>
                    _client.GetLocations(
                        latitude,
                        longitude,
                        page,
                        pageSize,
                        (page - 1) * pageSize,
                        distance, 
                        sortOrder));
        }

        if (locations == null)
        {
            return null;
        }

        return new LocalizationAirQuality
        {
            LocalizationName = localizationName,
            Latitude = latitude,
            Longitude = longitude,
            MeasurementStations = ParseMeasurementsStations(locations)
        };
    }

    private static async Task<IEnumerable<LocationDto>> GetAllResults(Func<int, Task<GetLocationsDto>> fetchRequest)
    {
        var page = 1;
        var continueFetching = true;
        var locations = new List<LocationDto>();

        do
        {
            var result = await fetchRequest(page);
            if (!result?.Results.Any() ?? true)
            {
                return null;
            }

            var meta = result.Meta;
            if (meta.Page * meta.Limit >= meta.Found)
            {
                continueFetching = false;
            }

            locations.AddRange(result.Results);
            page++;

        } while (continueFetching);

        return locations;
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