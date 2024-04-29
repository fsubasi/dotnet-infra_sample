using System;
using Microsoft.Extensions.Logging;
using Moq;
using sample_app.Controllers;
using Xunit;
using sample_app;

public class WeatherForecastControllerTests
{
    private readonly WeatherForecastController _controller;
    private readonly Mock<ILogger<WeatherForecastController>> _mockLogger;

    public WeatherForecastControllerTests()
    {
        _mockLogger = new Mock<ILogger<WeatherForecastController>>();
        _controller = new WeatherForecastController(_mockLogger.Object);
    }

    [Fact]
    public void Get_ReturnsFiveWeatherForecasts()
    {
        // Act
        var result = _controller.Get();

        // Assert
        var items = Assert.IsType<WeatherForecast[]>(result);
        Assert.Equal(5, items.Length); // Expect 5 items since that's what our controller method is set to return
    }

    [Fact]
    public void Get_ReturnsWeatherForecasts_WithValidDates()
    {
        // Act
        var result = _controller.Get();

        // Assert
        foreach (var forecast in result)
        {
            var dateDifference = (forecast.Date - DateTime.Now).Days;
            Assert.True(dateDifference >= 0 && dateDifference <= 5, "Date should be within the next 0 to 5 days.");
        }
    }

}
