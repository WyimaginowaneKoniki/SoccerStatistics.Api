using Microsoft.EntityFrameworkCore;
using SoccerStatistics.Api.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerStatistics.Api.Database
{
    public class SoccerStatisticsDbContext :DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<TeamInMatchStats> Teams_in_match_stats { get; set; }
        public DbSet<InteractionBetweenPlayers> Interactions_between_players { get; set; }
        public SoccerStatisticsDbContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
        }
    }
}
