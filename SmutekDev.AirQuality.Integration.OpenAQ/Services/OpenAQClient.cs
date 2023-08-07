using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using SmutekDev.AirQuality.Integration.OpenAQ.Models.DTOs;

namespace SmutekDev.AirQuality.Integration.OpenAQ.Services;

internal class OpenAQClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;

    public OpenAQClient(HttpClient httpClient, ILogger<OpenAQClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<GetLocationsDto> GetLocations(string lat, string lng, int page = 1, int pageSize = 10, int skip = 0)
    {
        var url = $"locations?limit={pageSize}&page={page}&offset={skip}&coordinates={lat},{lng}&radius=10000";
        return await GetLocations(url);
    }

    public async Task<GetLocationsDto> GetLocations(string city, int page = 1, int pageSize = 10, int skip = 0)
    {
        var url = $"locations?limit={pageSize}&page={page}&offset={skip}&city={city}&radius=10000";
        return await GetLocations(url);
    }

    private async Task<GetLocationsDto> GetLocations(string url)
    {
        try
        {
            var response = await _httpClient.GetAsync(url);
            var stringResult = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to get successful results for {url}. Message from an API: {msg}", url,
                    stringResult);
                return null;
            }

            return JsonConvert.DeserializeObject<GetLocationsDto>(stringResult, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
        catch (HttpRequestException e)
        {
            _logger.LogError(e, "Error when getting locations for {url}", url);
            return null;
        }
    }
}