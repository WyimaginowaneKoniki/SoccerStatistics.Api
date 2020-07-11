using System;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Match
    {
        public int Id { get; set; }
        [ForeignKey("Stadium")]
        public int Stadium_id { get; set; }
        public virtual Stadium Stadium { get; set; }
        [ForeignKey("Round")]
        public int Round_id { get; set; }
        public virtual Round Round { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Team1")]
        public virtual Team Team1{ get; set; }
        [ForeignKey("Team2")]
        public virtual Team Team2 { get; set; }
    }
}
