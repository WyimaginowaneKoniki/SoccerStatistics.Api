using Microsoft.Extensions.Logging;
using SoccerStatistics.Api.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoccerStatistics.Api.Database
{
    public class DataInitializer : IDataInitializer
    {
        private readonly SoccerStatisticsDbContext _context;
        private readonly ILogger<DataInitializer> _logger;
        private readonly IFakeData _fakeData;

        public DataInitializer(SoccerStatisticsDbContext context, 
            ILogger<DataInitializer> logger, IFakeData fakeData)
        {
            _context = context;
            _logger = logger;
            _fakeData = fakeData;
        }

        public void Seed()
        {
            const int teamCount = 5;
            const int transferCount = 50;
            const int leagueCount = 5;

            var teams = _fakeData.GetFakeTeam().Generate(teamCount);
            var players = teams.SelectMany(x => x.Players);

            _context.Stadiums.AddRange(teams.Select(x => x.Stadium));
            _logger.LogTrace("Stadiums were added");

            _context.Players.AddRange(players);
            _logger.LogTrace("Players were added");

            _context.Teams.AddRange(teams);
            _logger.LogTrace("Teams were added");

            _context.Transfers.AddRange(_fakeData.GetFakeTransfer(players, teams).Generate(transferCount));
            _logger.LogTrace("Transfers were added");

            var leagues = _fakeData.GetFakeLeague(teams).Generate(leagueCount);

            _context.Leagues.AddRange(leagues);
            _logger.LogTrace("Leagues were added");

            _context.Rounds.AddRange(leagues.SelectMany(x => x.Rounds));
            _logger.LogTrace("Rounds were added");

            var matches = leagues.SelectMany(x => x.Rounds.SelectMany(y => y.Matches));
            var teamStats = matches.SelectMany(x => new List<TeamInMatchStats> { x.TeamOneStats, x.TeamTwoStats });
            _context.Teams_in_match_stats.AddRange(teamStats);
            _logger.LogTrace("Team stats in matches were added");

            _context.Matches.AddRange(matches);
            _logger.LogTrace("Matches were added");

            _context.Activities.AddRange(matches.SelectMany(x => x.Activities));
            _logger.LogTrace("Activities were added");

            _context.Interactions_between_players.AddRange(matches.SelectMany(x => x.InteractionsBetweenPlayers));
            _logger.LogTrace("Interactions were added");

            try
            {
                _logger.LogTrace("Attempting to save data in the database");
                _context.SaveChanges();
            }
            catch (Exception err)
            {
                _logger.LogError($"Exception message: {err.Message}");
                if(!(err.InnerException is null))
                    _logger.LogError($"InnerException message: {err.InnerException.Message}");

                _logger.LogWarning("Data was not saved");

                return;
            }

            // when everything went well
            _logger.LogInformation("Initialization proceeded success");
        }
    }
}
