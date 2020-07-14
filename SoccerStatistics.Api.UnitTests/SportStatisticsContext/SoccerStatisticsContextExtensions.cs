using SoccerStatistics.Api.Database;
using SoccerStatistics.Api.Database.Entities;
using System;

namespace SoccerStatistics.Api.UnitTests.SportStatisticsContext
{
    public static class SoccerStatisticsContextExtensions
    {
        public static void FillDatabaseWithPlayers(this SoccerStatisticsDbContext dbContext)
        {
            dbContext.Players.Add(
                new Player()
                {
                    Id = 1,
                    Name = "Lionel",
                    Surname = "Messi",
                    Height = 169,
                    Weight = 68,
                    Birthday = new DateTime(1987, 6, 23),
                    Nationality = "Argentina",
                    DominantLeg = "Left",
                    Nick = "La Pulga",
                    Number = 10
                });

            dbContext.Players.Add(
                new Player()
                {
                    Id = 2,
                    Name = "Cristiano",
                    Surname = "Rolando",
                    Height = 189,
                    Weight = 85,
                    Birthday = new DateTime(1985, 2, 5),
                    Nationality = "Portugal",
                    DominantLeg = "Right",
                    Nick = "CR7",
                    Number = 7
                });

            dbContext.Players.Add(
                new Player()
                {
                    Id = 3,
                    Name = "Michał",
                    Surname = "Pazdan",
                    Height = 180,
                    Weight = 78,
                    Birthday = new DateTime(1987, 9, 21),
                    Nationality = "Poland",
                    DominantLeg = "",
                    Nick = "Priest",
                    Number = 22
                });

            dbContext.SaveChanges();
        }
        public static void FillDatabaseWithTeams(this SoccerStatisticsDbContext dbContext)
        {
            dbContext.Teams.Add(
                new Team()
                {
                    Id = 1,
                    FullName = "Manchester United Football Club",
                    ShortName = "Manchester United",
                    City= "Stretford",
                    CreatedAt = 1878,
                    Coach = "Ole Gunnar Solskjær"
                });

            dbContext.Teams.Add(
                new Team()
                {
                    Id = 2,
                    FullName = "Real Madrid Club de Futbol",
                    ShortName = "Real Madrid",
                    City = "Madrid",
                    CreatedAt = 1902,
                    Coach = "Zinedine Zidane"
                });

            dbContext.Teams.Add(
                new Team()
                {
                    Id = 3,
                    FullName = "Futbol Club Barcelona",
                    ShortName = "FC Barcelona",
                    City = "Barcelona",
                    CreatedAt = 1899,
                    Coach = "Quique Setien"
                });

            dbContext.SaveChanges();
        }

        public static void FillDatabaseWithMatches(this SoccerStatisticsDbContext dbContext)
        {
            var stadium = new Stadium
            {
                Id = 1,
                Name = "Old Trafford",
                Country = "England",
                City = "Manchester",
                BuiltAt = 1910,
                Capacity = 75_797,
                FieldSize = "105:68",
                Cost = 151_233M,
                VipCapacity = 4000,
                IsForDisabled = true,
                Lighting = 100_000,
                Architect = "Archibald Leitch",
                IsNational = false
            };

            var team = new Team() { Id = 1, FullName = "Manchester United FC" };

            dbContext.Add(
                new Match
                {
                    Id = 1,
                    Stadium = stadium,
                    AmountOfFans = 60_123,
                    Date = new DateTime(2015, 3, 4),
                    MatchTeam1 = team,
                    MatchTeam2 = new Team() { Id = 2, FullName = "FC Trampkarze" }
                });

            dbContext.Add(
                new Match
                {
                    Id = 2,
                    Stadium = stadium,
                    AmountOfFans = 58_123,
                    Date = new DateTime(2015, 3, 14),
                    MatchTeam1 = new Team() { Id = 3, FullName = "Coco Jambo" },
                    MatchTeam2 = team
                });

            dbContext.SaveChanges();
        }
        public static void FillDatabaseWithLeagues(this SoccerStatisticsDbContext dbContext)
        {
            dbContext.Leagues.Add(
                new League()
                {
                  Id = 1,
                  Name = "Primera Division",
                  Country = "Spain",
                  Season = "2018/2019",
                  MVP = "Lionel Messi",
                  Winner = "FC Barcelona",
                  });

            dbContext.Leagues.Add(
                 new League()
                 {
                     Id = 2,
                     Name = "Serie A",
                     Country = "Italia",
                     Season = "2017/2018",
                     MVP = "Mauro Icardi",
                     Winner = "Juventus",
                 });


            dbContext.Leagues.Add(
                 new League()
                 {
                     Id = 3,
                     Name = "Lotto Ekstraklasa",
                     Country = "Poland",
                     Season = "2018/2019",
                     MVP = "Igor Angulo",
                     Winner = "Piast Gliwice",
                 });


            dbContext.SaveChanges();
        }
    }
}
