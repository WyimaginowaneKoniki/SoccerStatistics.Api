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
                    CreatedAt = new DateTime(1878, 1, 1),
                    Coach = "Ole Gunnar Solskjær"
                });

            dbContext.Teams.Add(
                new Team()
                {
                    Id = 2,
                    FullName = "Real Madrid Club de Futbol",
                    ShortName = "Real Madrid",
                    City = "Madrid",
                    CreatedAt = new DateTime(1902, 3, 6),
                    Coach = "Zinedine Zidane"
                });

            dbContext.Teams.Add(
                new Team()
                {
                    Id = 3,
                    FullName = "Futbol Club Barcelona",
                    ShortName = "FC Barcelona",
                    City = "Barcelona",
                    CreatedAt = new DateTime(1899, 11, 29),
                    Coach = "Quique Setien"
                });

            dbContext.SaveChanges();
        }
    }
}
