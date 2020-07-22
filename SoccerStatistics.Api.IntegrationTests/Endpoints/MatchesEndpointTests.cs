using Newtonsoft.Json;
using SoccerStatistics.Api.Core.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Xunit;

namespace SoccerStatistics.Api.IntegrationTests.Endpoints
{
    public class MatchesEndpointTests : IClassFixture<TestWebAppFactory>
    {
        protected readonly HttpClient _client;

        public MatchesEndpointTests(TestWebAppFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async void GetAllMatchesOk()
        {
            // Arrange
            IEnumerable<MatchBasicDTO> returnedMatches = Enumerable.Empty<MatchBasicDTO>();

            // Act
            var response = await _client.GetAsync("api/leagues");
            
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var deserializeErr = Record.Exception(()
                => returnedMatches = JsonConvert.DeserializeObject<IEnumerable<MatchBasicDTO>>(responseString));

            // Assert
            Assert.Null(deserializeErr);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(returnedMatches);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void GetMatchByIdOk(int id)
        {
            // Arrange
            MatchDTO returnedMatch = null;

            // Act
            var response = await _client.GetAsync($"api/leagues/{id}");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var deserializeErr = Record.Exception(()
                => returnedMatch = JsonConvert.DeserializeObject<MatchDTO>(responseString));

            // Assert
            Assert.Null(deserializeErr);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(returnedMatch);
        }

        [Fact]
        public async void GetMatchByIdNotFound()
        {
            var response = await _client.GetAsync("api/leagues/0");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
