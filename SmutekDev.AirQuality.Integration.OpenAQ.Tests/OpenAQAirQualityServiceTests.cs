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
        client.GetLocations("Test", 1, 100, 0, distance: Arg.Any<Distance>(), sortOrder: Arg.Any<SortOrder>()).Returns(Task.FromResult(new GetLocationsDto
        {
            Results = new List<LocationDto>
            {
                new() { Id = 1 },
                new() { Id = 2 }
            },
            Meta = new MetaDto
            {
                Found = 2,
                Limit = 10,
                Page = 1
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

    [Fact]
    public async Task GetAirQualityForLocalization_ManyPagesOfData_ReturnsAllItems()
    {
        //Arrange
        var client = Substitute.For<OpenAQClient>();
        client.GetLocations("Test", 1, 10, 0, distance: Arg.Any<Distance>(), sortOrder: Arg.Any<SortOrder>()).Returns(Task.FromResult(new GetLocationsDto
        {
            Results = new List<LocationDto>
            {
                new() { Id = 1 },
                new() { Id = 2 },
                new() { Id = 3 },
                new() { Id = 4 },
                new() { Id = 5 },
                new() { Id = 6 },
                new() { Id = 7 },
                new() { Id = 8 },
                new() { Id = 9 },
                new() { Id = 10 },
            },
            Meta = new MetaDto
            {
                Found = 15,
                Limit = 10,
                Page = 1
            }
        }));
        client.GetLocations("Test", 2, 10, 10, distance: Arg.Any<Distance>(), sortOrder: Arg.Any<SortOrder>()).Returns(Task.FromResult(new GetLocationsDto
        {
            Results = new List<LocationDto>
            {
                new() { Id = 11 },
                new() { Id = 12 },
                new() { Id = 13 },
                new() { Id = 14 },
                new() { Id = 15 }
            },
            Meta = new MetaDto
            {
                Found = 15,
                Limit = 10,
                Page = 2
            }
        }));

        var service = new OpenAQAirQualityService(client);

        //Act
        var results = await service.GetAirQualityForLocalization(new GetAirQualityParams
        {
            Localization = "Test",
            PageSize = 10
        });

        //Assert
        Assert.NotNull(results);
        Assert.Equal(15, results.MeasurementStations.Count());
    }
}