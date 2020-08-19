using Newtonsoft.Json;
using SoccerStatistics.Api.Core.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Xunit;

namespace SoccerStatistics.Api.IntegrationTests.Endpoints
{
    public class LeaguesEndpointTests : IClassFixture<TestWebAppFactory>
    {
        protected readonly HttpClient _client;

        public LeaguesEndpointTests(TestWebAppFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async void GetAllLeaguesOk()
        {
            // Arrange
            IEnumerable<LeagueBasicDTO> returnedLeagues = Enumerable.Empty<LeagueBasicDTO>();

            // Act
            var response = await _client.GetAsync("api/leagues");
            
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var deserializeErr = Record.Exception(()
                => returnedLeagues = JsonConvert.DeserializeObject<IEnumerable<LeagueBasicDTO>>(responseString));


            // Assert
            Assert.Null(deserializeErr);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(returnedLeagues);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void GetLeagueByIdOk(int id)
        {
            // Arrange
            LeagueDTO returnedLeague = null;

            // Act
            var response = await _client.GetAsync($"api/leagues/{id}");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var deserializeErr = Record.Exception(()
                => returnedLeague = JsonConvert.DeserializeObject<LeagueDTO>(responseString));


            // Assert
            Assert.Null(deserializeErr);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(returnedLeague);
        }

        [Fact]
        public async void GetLeagueByIdNotFound()
        {
            var response = await _client.GetAsync("api/leagues/0");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
