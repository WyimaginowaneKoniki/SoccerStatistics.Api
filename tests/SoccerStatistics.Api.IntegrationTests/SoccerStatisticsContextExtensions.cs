using SoccerStatistics.Api.Database;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoccerStatistics.Api.IntegrationTests
{
    public static class SportStatisticsContextExtensions
    {
        public static void FillDatabase(this SoccerStatisticsDbContext context)
        {
            const int teamCount = 12;
            const int leagueCount = 3;

            var teams = FakeData.GetFakeTeam().Generate(teamCount);
            var players = teams.SelectMany(x => x.Players);
            var leagues = FakeData.GetFakeLeague(teams).Generate(leagueCount);
            var matches = leagues.SelectMany(x => x.Rounds.SelectMany(y => y.Matches));
            var teamStats = matches.SelectMany(x => new List<TeamInMatchStats> { x.TeamOneStats, x.TeamTwoStats });

            context.Stadiums.AddRange(teams.Select(x => x.Stadium));
            context.Players.AddRange(players);
            context.Teams.AddRange(teams);
            context.Leagues.AddRange(leagues);
            context.Rounds.AddRange(leagues.SelectMany(x => x.Rounds));
            context.Teams_in_match_stats.AddRange(teamStats);
            context.Matches.AddRange(matches);
            context.Activities.AddRange(matches.SelectMany(x => x.Activities));
            context.Interactions_between_players.AddRange(matches.SelectMany(x => x.InteractionsBetweenPlayers));

            context.SaveChanges();
        }
    }
}