using Microsoft.EntityFrameworkCore;
using SoccerStatistics.Api.Database.Entities;

namespace SoccerStatistics.Api.Database
{
    public class SoccerStatisticsDbContext : DbContext
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
        public DbSet<TeamInLeague> Team_in_league { get; set; }
        public DbSet<Formation> Formations { get; set; }
        public DbSet<Bench> Benches { get; set; }
        public DbSet<ExtraTime> ExtraTimes { get; set; }
        public DbSet<Overtime> Overtimes { get; set; }
        public DbSet<PenaltyKick> PenaltyKicks { get; set; }

        public SoccerStatisticsDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Match>(e =>
            {
                e.HasOne(m => m.TeamOneStats)
                 .WithOne()
                 .HasForeignKey<Match>("TeamOneStatsId")
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(m => m.TeamTwoStats)
                 .WithOne()
                 .HasForeignKey<Match>("TeamTwoStatsId")
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<InteractionBetweenPlayers>(e =>
            {
                e.HasOne(i => i.Player1)
                 .WithMany()
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(i => i.Player2)
                 .WithMany()
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<PenaltyKick>(e =>
            {
                e.HasOne(i => i.Shooter)
                 .WithMany()
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);
                e.HasOne(i => i.Goalkeeper)
                 .WithMany()
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
