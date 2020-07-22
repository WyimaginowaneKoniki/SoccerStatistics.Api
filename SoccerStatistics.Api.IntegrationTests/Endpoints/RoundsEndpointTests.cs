using Newtonsoft.Json;
using SoccerStatistics.Api.Core.DTO;
using System.Net;
using System.Net.Http;
using Xunit;

namespace SoccerStatistics.Api.IntegrationTests.Endpoints
{
    public class RoundsEndpointTests : IClassFixture<TestWebAppFactory>
    {
        protected readonly HttpClient _client;

        public RoundsEndpointTests(TestWebAppFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void GetRoundByIdOk(int id)
        {
            // Arrange
            RoundDTO returnedRound = null;

            // Act
            var response = await _client.GetAsync($"api/rounds/{id}");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var deserializeErr = Record.Exception(()
                => returnedRound = JsonConvert.DeserializeObject<RoundDTO>(responseString));

            // Assert
            Assert.Null(deserializeErr);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(returnedRound);
        }

        [Fact]
        public async void GetRoundByIdNotFound()
        {
            var response = await _client.GetAsync("api/rounds/0");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
