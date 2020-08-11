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
        private static readonly IEnumerable<Match> matches = new List<Match>()
        {
                new Match
                {
                    Id = 1,
                    AmountOfFans = 60_123,
                    Date = new DateTime(2015, 3, 4),
                },
                new Match
                {
                    Id = 2,
                    AmountOfFans = 58_123,
                    Date = new DateTime(2015, 3, 14)
                }
        };

        private static readonly IEnumerable<Player> players = new List<Player>()
        {
            new Player()
            {
                Id = 1,
                Name = "Lionel",
                Surname = "Messi",
                Height = 169,
                Weight = 68,
                Birthday = new DateTime(1987, 6, 23),
                Nationality = "Argentina",
                DominantLeg = DominantLegType.Left,
                Nick = "La Pulga",
                Number = 10
            },
            new Player()
            {
                Id = 2,
                Name = "Cristiano",
                Surname = "Rolando",
                Height = 189,
                Weight = 85,
                Birthday = new DateTime(1985, 2, 5),
                Nationality = "Portugal",
                DominantLeg = DominantLegType.Right,
                Nick = "CR7",
                Number = 7
            },
            new Player()
            {
                Id = 3,
                Name = "Michał",
                Surname = "Pazdan",
                Height = 180,
                Weight = 78,
                Birthday = new DateTime(1987, 9, 21),
                Nationality = "Poland",
                DominantLeg = DominantLegType.Undefined,
                Nick = "Priest",
                Number = 22
            }
        };

        private static readonly IEnumerable<Team> teams = new List<Team>()
        {
            new Team()
            {
                Id = 1,
                FullName = "Manchester United Football Club",
                ShortName = "Manchester United",
                City = "Stretford",
                CreatedAt = 1878,
                Coach = "Ole Gunnar Solskjær"
            },
            new Team()
            {
                Id = 2,
                FullName = "Real Madrid Club de Futbol",
                ShortName = "Real Madrid",
                City = "Madrid",
                CreatedAt = 1902,
                Coach = "Zinedine Zidane"
            },
            new Team()
            {
                Id = 3,
                FullName = "Futbol Club Barcelona",
                ShortName = "FC Barcelona",
                City = "Barcelona",
                CreatedAt = 1899,
                Coach = "Quique Setien"
            }
        };

        private static readonly IEnumerable<Round> rounds = new List<Round>()
        {
            new Round()
            {
                Id = 1,
                Name = "Round 1",

            },
            new Round()
            {
                Id = 2,
                Name = "Round 2",
            },
            new Round()
            {
                Id = 3,
                Name = "Round 3",
            }
        };

        private static readonly IEnumerable<Stadium> stadiums = new List<Stadium>()
        {
            new Stadium
            {
                Id = 1,
                Name = "Old Trafford",
                Country = "England",
                City = "Manchester",
                BuiltAt = 1910,
                Capacity = 75_797,
                FieldSize = "105:68",
                VipCapacity = 4000,
                IsForDisabled = true,
                Lighting = 100_000,
                Architect = "Archibald Leitch",
                IsNational = false
            },
            new Stadium
            {
                Id = 2,
                Name = "Camp Nou",
                Country = "Spain",
                City = "Barcelona",
                BuiltAt = 1957,
                Capacity = 99_354,
                FieldSize = "105:68",
                VipCapacity = 400,
                IsForDisabled = false,
                Lighting = 1770,
                Architect = "Francesc Mijtans-Miro",
                IsNational = false
            },
            new Stadium
            {
                Id = 3,
                Name = "Estadio Nacional de Brasilia Mane Garrincha",
                Country = "Brasilia",
                City = "Brasilia",
                BuiltAt = 2013,
                Capacity = 72_888,
                FieldSize = "105:68",
                VipCapacity = 110,
                IsForDisabled = true,
                Lighting = 1770,
                Architect = "Castro Mello Architects",
                IsNational = true
            }
        };

        private static readonly IEnumerable<League> leagues = new List<League>()
        {
            new League()
            {
                Id = 1,
                Name = "Primera Division",
                Country = "Spain",
                Season = "2018/2019",
                MVP = players.ElementAt(0),
                Winner = teams.ElementAt(0)
            },
            new League()
            {
                Id = 2,
                Name = "Serie A",
                Country = "Italia",
                Season = "2017/2018",
                MVP = players.ElementAt(1),
                Winner = teams.ElementAt(1)
            },
            new League()
            {
                Id = 3,
                Name = "Lotto Ekstraklasa",
                Country = "Poland",
                Season = "2018/2019",
                MVP = players.ElementAt(2),
                Winner = teams.ElementAt(2)
            }
        };

        public async static void FillDatabase(this SoccerStatisticsDbContext context)
        {
            await context.AddRangeAsync(leagues);
            await context.AddRangeAsync(matches);
            await context.AddRangeAsync(players);
            await context.AddRangeAsync(teams);
            await context.AddRangeAsync(rounds);
            await context.AddRangeAsync(stadiums);

            await context.SaveChangesAsync();
        }
    }
}