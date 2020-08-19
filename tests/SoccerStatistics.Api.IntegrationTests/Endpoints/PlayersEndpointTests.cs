using Newtonsoft.Json;
using SoccerStatistics.Api.Core.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Xunit;

namespace SoccerStatistics.Api.IntegrationTests.Endpoints
{
    public class PlayersEndpointTests : IClassFixture<TestWebAppFactory>
    {
        protected readonly HttpClient _client;

        public PlayersEndpointTests(TestWebAppFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async void GetAllPlayersOk()
        {
            // Arrange
            IEnumerable<PlayerBasicDTO> returnedPlayers = Enumerable.Empty<PlayerBasicDTO>();

            // Act
            var response = await _client.GetAsync("api/players");
            
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var deserializeErr = Record.Exception(()
                => returnedPlayers = JsonConvert.DeserializeObject<IEnumerable<PlayerBasicDTO>>(responseString));

            // Assert
            Assert.Null(deserializeErr);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(returnedPlayers);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void GetPlayerByIdOk(int id)
        {
            // Arrange
            PlayerDTO returnedPlayer = null;

            // Act
            var response = await _client.GetAsync($"api/players/{id}");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var deserializeErr = Record.Exception(()
                => returnedPlayer = JsonConvert.DeserializeObject<PlayerDTO>(responseString));

            // Assert
            Assert.Null(deserializeErr);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(returnedPlayer);
        }

        [Fact]
        public async void GetPlayerByIdNotFound()
        {
            var response = await _client.GetAsync("api/players/0");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
