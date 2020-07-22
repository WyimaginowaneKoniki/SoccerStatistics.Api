using Newtonsoft.Json;
using SoccerStatistics.Api.Core.DTO;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xunit;

namespace SoccerStatistics.Api.IntegrationTests.Endpoints
{
    public class StadiumsEndpointTests : IClassFixture<TestWebAppFactory>
    {
        protected readonly HttpClient _client;

        public StadiumsEndpointTests(TestWebAppFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async void GetAllStadiumsOk()
        {
            // Arrange
            IEnumerable<StadiumBasicDTO> returnedStadiums = null;

            // Act
            var response = await _client.GetAsync("api/stadiums");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var deserializeErr = Record.Exception(()
                => returnedStadiums = JsonConvert.DeserializeObject<IEnumerable<StadiumBasicDTO>>(responseString));

            // Assert
            Assert.Null(deserializeErr);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(returnedStadiums);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void GetStadiumByIdOk(int id)
        {
            // Arrange
            StadiumDTO returnedStadium = null;

            // Act
            var response = await _client.GetAsync($"api/stadiums/{id}");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var deserializeErr = Record.Exception(()
                => returnedStadium = JsonConvert.DeserializeObject<StadiumDTO>(responseString));

            // Assert
            Assert.Null(deserializeErr);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(returnedStadium);
        }

        [Fact]
        public async void GetStadiumByIdNotFound()
        {
            var response = await _client.GetAsync("api/stadiums/0");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
