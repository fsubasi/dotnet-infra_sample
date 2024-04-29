using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Text.Json;
using sample_app; // Make sure this using directive matches the namespace of your Startup class

namespace sample_app.IntegrationTests
{
    public class WeatherForecastControllerIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public WeatherForecastControllerIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_WeatherForecast_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/WeatherForecast");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        [Fact]
        public async Task Get_WeatherForecast_ReturnsExpectedNumberOfForecasts()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/WeatherForecast");

            // Assert
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var forecasts = JsonSerializer.Deserialize<WeatherForecast[]>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(forecasts); // Ensure forecasts is not null
            if (forecasts != null) // This check makes the code more robust and null-safe.
            {
                Assert.Equal(5, forecasts.Length); // Verify the expected count
            }
        }
    }
}
