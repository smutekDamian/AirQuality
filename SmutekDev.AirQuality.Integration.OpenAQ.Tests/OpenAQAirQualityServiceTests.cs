using NSubstitute;
using SmutekDev.AirQuality.Core.Models;
using SmutekDev.AirQuality.Integration.OpenAQ.Models.DTOs;
using SmutekDev.AirQuality.Integration.OpenAQ.Services;

namespace SmutekDev.AirQuality.Integration.OpenAQ.Tests;

public class OpenAQAirQualityServiceTests
{
    [Fact]
    public async Task GetAirQualityForLocalization_OnePageOfData_ParsesAndReturnsResults()
    {
        //Arrange
        var client = Substitute.For<OpenAQClient>();
        client.GetLocations("Test", distance: Arg.Any<Distance>(), sortOrder: Arg.Any<SortOrder>()).Returns(Task.FromResult(new GetLocationsDto
        {
            Results = new List<LocationDto>
            {
                new() { Id = 1 },
                new() { Id = 2 }
            }
        }));
        var service = new OpenAQAirQualityService(client);

        //Act
        var results = await service.GetAirQualityForLocalization(new GetAirQualityParams
        {
            Localization = "Test"
        });

        //Assert
        Assert.NotNull(results);
        Assert.Equal(2, results.MeasurementStations.Count());
    }
}