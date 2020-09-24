using Bogus;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoccerStatistics.Api.Database
{
    public class FakeData : IFakeData
    {
        #region Id
        private uint stadiumId = 1;
        private uint playerId = 1;
        private uint teamId = 1;
        private uint transferId = 1;
        private uint activityId = 1;
        private uint interactionBetweenPlayersId = 1;
        private uint teamInMatchStatsId = 1;
        private uint matchId = 1;
        private uint roundId = 1;
        private uint teamInLeagueId = 1;
        private uint leagueId = 1;
        private uint benchId = 1;
        private uint formationId = 1;
        private uint extraTimeId = 1;
        private uint overtimeId = 1;
        private uint penaltyKickId = 1;
        #endregion

        public Faker<Player> GetFakePlayer()
        {
            const uint minHeight = 150, maxHeight = 220;
            const uint minWeight = 50, maxWeight = 140;
            const uint minNumber = 0, maxNumber = 99;
            const int minAge = 16, maxAge = 44;

            return new Faker<Player>()
                .RuleFor(p => p.Id, f => playerId++)
                .RuleFor(p => p.Name, f => f.Name.FirstName())
                .RuleFor(p => p.Surname, f => f.Name.LastName())
                .RuleFor(p => p.Height, f => f.Random.UInt(minHeight, maxHeight))
                .RuleFor(p => p.Weight, f => f.Random.UInt(minWeight, maxWeight))
                .RuleFor(p => p.Position, f => f.PickRandom<PlayerPosition>())
                .RuleFor(p => p.Birthday, f => f.Date.Past(maxAge,
                                                            DateTime.Now.AddYears(-minAge)))
                .RuleFor(p => p.Nationality, f => f.Address.Country())
                .RuleFor(p => p.DominantLeg, f => f.PickRandom<DominantLegType>())
                .RuleFor(p => p.Nick, (f, p) => f.Internet.UserName(p.Surname))
                .RuleFor(p => p.Number, f => f.Random.UInt(minNumber, maxNumber));
        }

        public Faker<Stadium> GetFakeStadium()
        {
            const uint minBuildYear = 1900;
            const uint minCapacity = 0, maxCapacity = 100_000;
            const uint minCost = 10_000, maxCost = 1_000_000;
            const uint minVipCapacity = 0;
            const uint minLighting = 0, maxLighting = 100_000;

            return new Faker<Stadium>()
                .RuleFor(s => s.Id, f => stadiumId++)
                .RuleFor(s => s.Name, f => $"{f.Company.CompanyName()} Stadium")
                .RuleFor(s => s.Country, f => f.Address.Country())
                .RuleFor(s => s.City, f => f.Address.City())
                .RuleFor(s => s.BuiltAt, f => f.Random.UInt(minBuildYear, (uint)DateTime.Now.Year))
                .RuleFor(s => s.Capacity, f => f.Random.UInt(minCapacity, maxCapacity))
                .RuleFor(s => s.FieldSize, f => f.Random.Replace("##x##"))
                .RuleFor(s => s.Cost, f => f.Finance.Amount(minCost, maxCost))
                // overall capacity includes VIP seats
                .RuleFor(s => s.VipCapacity, (f, s) => f.Random.UInt(minVipCapacity, s.Capacity))
                .RuleFor(s => s.IsForDisabled, f => f.Random.Bool())
                .RuleFor(s => s.Lighting, f => f.Random.UInt(minLighting, maxLighting))
                .RuleFor(s => s.Architect, f => f.Name.FullName())
                .RuleFor(s => s.IsNational, f => f.Random.Bool());
        }

        public Faker<Team> GetFakeTeam()
        {
            const uint minBuildYear = 1900;
            const int minPlayers = 12, maxPlayers = 30;

            return new Faker<Team>()
                .RuleFor(t => t.Id, f => teamId++)
                .RuleFor(t => t.ShortName, f => f.Company.CompanyName(2))
                .RuleFor(t => t.FullName,
                         (f, l) => f.Company.CompanyName($"{l.ShortName} {f.Company.CompanySuffix()}"))
                .RuleFor(t => t.CreatedAt, f => f.Random.UInt(minBuildYear, (uint)DateTime.Now.Year))
                .RuleFor(t => t.Coach, f => f.Name.FullName())
                .RuleFor(t => t.City, f => f.Address.City())
                .RuleFor(t => t.Stadium, f => GetFakeStadium())
                .RuleFor(t => t.Players,
                         f => GetFakePlayer().Generate(f.Random.Int(minPlayers, maxPlayers)));
        }

        /// Transfers must operate on players and teams that already exist in db
        public Faker<Transfer> GetFakeTransfer(IEnumerable<Player> players,
                                               IEnumerable<Team> teams)
        {
            const decimal minPrice = 100, maxPrice = 10_000_000;
            const int oldestTrasferYear = 5;

            return new Faker<Transfer>()
                .RuleFor(t => t.Id, f => transferId++)
                .RuleFor(t => t.Price, f => f.Finance.Amount(minPrice, maxPrice))
                // get transfer's date between oldestTrasferYear and now
                .RuleFor(t => t.Date, f => f.Date.Past(oldestTrasferYear))
                .RuleFor(t => t.PlayerPosition, f => f.PickRandom<PlayerPosition>())
                .RuleFor(t => t.Player, f => f.PickRandom(players))
                .RuleFor(t => t.SourceTeam, f => f.PickRandom(teams))
                // player should go to different team
                .RuleFor(t => t.DestTeam,
                         (f, t) => f.PickRandom(teams.Where(x => x.Id != t.SourceTeam.Id)));
        }

        public Faker<Match> GetFakeMatch(IEnumerable<Team> teams)
        {
            const int minActivity = 2, maxActivity = 20;
            const int minInteraction = 2, maxInteraction = 20;
            const int yearsToTheEarliestMatch = 20, daysToTheLatestMatch = 30;

            return new Faker<Match>()
                .RuleFor(m => m.Id, f => matchId++)
                .RuleFor(m => m.Stadium, GetFakeStadium())
                .RuleFor(m => m.AmountOfFans, (f, s) => f.Random.UInt(s.Stadium.Capacity))
                .RuleFor(m => m.Date, f => f.Date.Past(yearsToTheEarliestMatch, DateTime.Now.AddDays(daysToTheLatestMatch)))
                // Pick random team (from league) and generate stats 
                .RuleFor(m => m.TeamOneStats, f => GetFakeTeamInMatchStats(f.PickRandom(teams)))
                .RuleFor(m => m.TeamTwoStats, f => GetFakeTeamInMatchStats(f.PickRandom(teams)))
                // Take all players from two team, then generate activities they can perform
                .RuleFor(m => m.Activities,
                        (f, m) => GetFakeActivity(m.TeamOneStats.Team.Players.Concat(m.TeamTwoStats.Team.Players))
                                    .Generate(f.Random.Int(minActivity, maxActivity)))
                .RuleFor(m => m.InteractionsBetweenPlayers,
                        (f, m) => GetFakeInteraction(m.TeamOneStats.Team.Players.Concat(m.TeamTwoStats.Team.Players))
                                    .Generate(f.Random.Int(minInteraction, maxInteraction)))
                .RuleFor(m => m.ExtraTime, f => GetFakeExtraTime(45, 90).Generate(3))
                .RuleFor(m => m.Overtime, (f, m) => GetFakeOverTime(m.TeamOneStats.Team.Players.Concat(m.TeamTwoStats.Team.Players)).Generate());
        }

        public Faker<Activity> GetFakeActivity(IEnumerable<Player> players)
        {
            const uint timeOfTheEarliestActivity = 0, timeOfTheLatestActivity = 125;

            return new Faker<Activity>()
                .RuleFor(a => a.Id, f => activityId++)
                .RuleFor(a => a.ActivityType, f => f.PickRandom<ActivityType>())
                .RuleFor(a => a.TimeAt, f => f.Random.UInt(timeOfTheEarliestActivity,
                                                           timeOfTheLatestActivity))
                .RuleFor(a => a.Player, f => f.PickRandom(players))
                .RuleFor(a => a.Description, (f, a) => $"{a.Player.Name} {a.Player.Surname} {f.Hacker.Verb()}");
        }

        public Faker<InteractionBetweenPlayers> GetFakeInteraction(IEnumerable<Player> players)
        {
            const uint timeOfTheEarliestActivity = 0, timeOfTheLatestActivity = 125;

            return new Faker<InteractionBetweenPlayers>()
                .RuleFor(i => i.Id, f => interactionBetweenPlayersId++)
                .RuleFor(i => i.InteractionType, f => f.PickRandom<InteractionType>())
                .RuleFor(i => i.TimeAt, f => f.Random.UInt(timeOfTheEarliestActivity,
                                                           timeOfTheLatestActivity))
                .RuleFor(i => i.Player1, f => f.PickRandom(players))
                .RuleFor(i => i.Player2, (f, i) => f.PickRandom(players.Where(x => x.Id != i.Player1.Id)))
                .RuleFor(i => i.Description,
                         (f, i) => $"{i.Player1.Name} {i.Player1.Surname} {f.Hacker.Verb()} {i.Player2.Name} {i.Player2.Surname}");
        }

        public Faker<TeamInMatchStats> GetFakeTeamInMatchStats(Team team)
        {
            const uint minPassCount = 0, maxPassCount = 2_000;
            const uint minSuccessPassCount = 0;
            const uint minPercentOfBallPossesion = 0, maxPercentOfBallPossesion = 100;

            return new Faker<TeamInMatchStats>()
                .RuleFor(t => t.Id, f => teamInMatchStatsId++)
                .RuleFor(t => t.Pass, f => f.Random.UInt(minPassCount, maxPassCount))
                .RuleFor(t => t.PassSuccess, (f, t) => f.Random.UInt(minSuccessPassCount, t.Pass - 1))
                .RuleFor(t => t.BallPossesion, f => f.Random.UInt(minPercentOfBallPossesion,
                                                                  maxPercentOfBallPossesion))
                .RuleFor(t => t.Formation, f => GenerateFormation())
                .RuleFor(t => t.Team, f => team)
                .RuleFor(t => t.PlayersOnBench, f => GetFakeBench(team.Players).Generate(10))
                .RuleFor(t => t.PlayersInFormation, f => GetFakePlayersInFormation(team.Players).Generate(10));
        }

        public Faker<Bench> GetFakeBench(IEnumerable<Player> players)
        => new Faker<Bench>()
                .RuleFor(b => b.Id, f => benchId++)
                .RuleFor(b => b.Player, f => f.PickRandom(players));

        public Faker<Formation> GetFakePlayersInFormation(IEnumerable<Player> players)
        => new Faker<Formation>()
                .RuleFor(fo => fo.Id, f => formationId++)
                .RuleFor(fo => fo.Player, f => f.PickRandom(players))
                .RuleFor(fo => fo.PositionNumber, f => f.Random.UInt(0, 10));

        public Faker<ExtraTime> GetFakeExtraTime(uint minTimeAt, uint maxTimeAt)
        => new Faker<ExtraTime>()
                .RuleFor(e => e.Id, f => extraTimeId++)
                .RuleFor(e => e.AdditionalTime, f => f.Random.UInt(1, 5))
                .RuleFor(e => e.TimeAt, f => f.Random.UInt(minTimeAt, maxTimeAt));

        public Faker<Overtime> GetFakeOverTime(IEnumerable<Player> players)
        => new Faker<Overtime>()
                .RuleFor(o => o.Id, f => overtimeId++)
                .RuleFor(o => o.ExtraTime, f => GetFakeExtraTime(15, 30).Generate(2))
                .RuleFor(o => o.PenaltyKicks, f => GetFakePenaltyKick(players).Generate(10));

        public Faker<PenaltyKick> GetFakePenaltyKick(IEnumerable<Player> players)
        => new Faker<PenaltyKick>()
                .RuleFor(o => o.Id, f => penaltyKickId++)
                .RuleFor(o => o.Shooter, f => f.PickRandom(players))
                .RuleFor(o => o.Goalkeeper, f => f.PickRandom(players))
                .RuleFor(o => o.IsGoal, f => f.Random.Bool());

        public Faker<Round> GetFakeRound(League league)
        {
            const int minMatches = 2, maxMatches = 5;

            return new Faker<Round>()
                .RuleFor(r => r.Id, f => roundId++)
                .RuleFor(r => r.League, f => league)
                .RuleFor(r => r.Name, f => f.Random.Replace("Round ##"))
                .RuleFor(r => r.Matches,
                         f => GetFakeMatch(league.Teams.Select(x => x.Team))
                                .Generate(f.Random.Int(minMatches, maxMatches)));
        }

        public Faker<League> GetFakeLeague(IEnumerable<Team> teams)
        {
            const int minTeamCount = 4, maxTeamCount = 10;
            const int minRoundsCount = 5, maxRoundsCount = 20;
            const int yearOfTheEarliestSeason = 1900;

            return new Faker<League>()
                .RuleFor(l => l.Id, f => leagueId++)
                .RuleFor(l => l.Shortname, f => f.Company.CompanyName(2))
                .RuleFor(l => l.Name, (f, l) => f.Company.CompanyName(l.Name + f.Company.CompanySuffix()))
                .RuleFor(l => l.Country, f => f.Address.Country())
                .RuleFor(l => l.Season, f => f.Random.Number(yearOfTheEarliestSeason, DateTime.Now.Year).ToString())
                .RuleFor(l => l.MVP, f => f.Random.Bool() ? f.PickRandom(teams.SelectMany(x => x.Players)) : null)
                .RuleFor(l => l.Winner, (f, l) => l.MVP != null ? f.PickRandom(teams) : null)
                // Generate randomize collection of teams in league
                .RuleFor(l => l.Teams, (f, m) => Enumerable.Range(minTeamCount, maxTeamCount)
                                                           .Select(x => GetFakeTeamInLeague(m, f.PickRandom(teams)).Generate())
                                                           .ToList())
                .RuleFor(l => l.Rounds, (f, m) => GetFakeRound(m).Generate(f.Random.Int(minRoundsCount, maxRoundsCount)));
        }

        public Faker<TeamInLeague> GetFakeTeamInLeague(League league, Team team)
        => new Faker<TeamInLeague>()
                .RuleFor(t => t.Id, f => teamInLeagueId++)
                .RuleFor(t => t.League, f => league)
                .RuleFor(t => t.Team, f => team);

        private string GenerateFormation()
        {
            var rnd = new Random();

            List<int> formation = new List<int>();
            do
            {
                int formationCount = 10;
                formation.Clear();

                do
                {
                    // get random number in range <1, 11)
                    var line = rnd.Next(1, formationCount + 1);

                    if (line >= 8)
                        continue;

                    formationCount -= line;

                    formation.Add(line);

                } while (formationCount != 0);
            } while (formation.Count < 3 && formation.Count > 5);

            return string.Join("-", formation.Select(x => x.ToString()));
        }
    }
}
