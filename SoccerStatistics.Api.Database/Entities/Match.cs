using System;
using System.Collections.Generic;

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
        [ForeignKey("Team_in_match_stats")]
        public virtual Team_in_match_stats Team_in_match_stats { get; set; }
        [ForeignKey("Activity")]
        public int Activity_id { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
    }
}
