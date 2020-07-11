using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Match
    {
        public int Id { get; set; }
        [ForeignKey("Stadium")]
        public virtual Stadium Stadium { get; set; }
        public uint AmountOfFans { get; set; }
        [ForeignKey("Round")]
        public virtual Round Round { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [ForeignKey("Team_in_match_stats")]
        public virtual TeamInMatchStats TeamInMatchStats { get; set; }
        [ForeignKey("Activity")]
        public virtual IEnumerable<Activity> Activities { get; set; }
        public virtual IEnumerable<InteractionBetweenPlayers> InteractionBetweenPlayers { get; set; }
    }
}
