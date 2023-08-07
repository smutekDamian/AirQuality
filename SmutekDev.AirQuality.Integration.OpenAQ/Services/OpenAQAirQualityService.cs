using SmutekDev.AirQuality.Core.Models;
using SmutekDev.AirQuality.Core.Services;
using SmutekDev.AirQuality.Integration.OpenAQ.Models;
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

        if (CoordinatesAreMissing(parameters))
        {
            locations = await GetLocationsByCity(parameters);
        }
        else
        {
            locations = await GetLocationsByCoordinates(parameters);
        }

        if (locations == null)
        {
            return null;
        }

        return new LocalizationAirQuality
        {
            LocalizationName = parameters.Localization,
            Latitude = parameters.Lat,
            Longitude = parameters.Lng,
            MeasurementStations = ParseMeasurementsStations(locations)
        };
    }

    private static bool CoordinatesAreMissing(GetAirQualityParams parameters)
    {
        return string.IsNullOrWhiteSpace(parameters.Lat) || string.IsNullOrWhiteSpace(parameters.Lng);
    }

    private async Task<IEnumerable<LocationDto>> GetLocationsByCoordinates(GetAirQualityParams parameters)
    {
        return await GetAllResults(page =>
            _client.GetLocationsByCoordinates(
                parameters.Lat,
                parameters.Lng,
                new FilteringParameters
                {
                    Page = page,
                    PageSize = parameters.PageSize,
                    Skip = (page - 1) * parameters.PageSize,
                    Distance = parameters.Distance,
                    SortOrder = parameters.SortOrder
                }));
    }

    private async Task<IEnumerable<LocationDto>> GetLocationsByCity(GetAirQualityParams parameters)
    {
        return await GetAllResults(page => 
            _client.GetLocationsByCity(
                parameters.Localization,
                new FilteringParameters
                {
                    Page = page,
                    PageSize = parameters.PageSize,
                    Skip = (page - 1) * parameters.PageSize,
                    Distance = parameters.Distance,
                    SortOrder = parameters.SortOrder
                }));
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