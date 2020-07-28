using Newtonsoft.Json;
using SoccerStatistics.Api.Core.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Xunit;

namespace SoccerStatistics.Api.IntegrationTests.Endpoints
{
    public class TeamsEndpointTests : IClassFixture<TestWebAppFactory>
    {
        protected readonly HttpClient _client;

        public TeamsEndpointTests(TestWebAppFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async void GetAllTeamsOk()
        {
            // Arrange
            IEnumerable<TeamBasicDTO> returnedTeams = Enumerable.Empty<TeamBasicDTO>();

            // Act
            var response = await _client.GetAsync("api/teams");
            
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var deserializeErr = Record.Exception(()
                => returnedTeams = JsonConvert.DeserializeObject<IEnumerable<TeamBasicDTO>>(responseString));


            // Assert
            Assert.Null(deserializeErr);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(returnedTeams);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void GetTeamByIdOk(int id)
        {
            // Arrange
            TeamDTO returnedTeam = null;

            // Act
            var response = await _client.GetAsync($"api/teams/{id}");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var deserializeErr = Record.Exception(()
                => returnedTeam = JsonConvert.DeserializeObject<TeamDTO>(responseString));


            // Assert
            Assert.Null(deserializeErr);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(returnedTeam);
        }

        [Fact]
        public async void GetTeamByIdNotFound()
        {
            var response = await _client.GetAsync("api/teams/0");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
