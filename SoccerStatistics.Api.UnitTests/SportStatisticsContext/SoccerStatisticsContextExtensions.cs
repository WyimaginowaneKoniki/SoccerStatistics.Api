using SoccerStatistics.Api.Database;
using SoccerStatistics.Api.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
