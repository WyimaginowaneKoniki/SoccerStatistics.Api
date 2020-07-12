using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public virtual Stadium Stadium { get; set; }
        public uint AmountOfFans { get; set; }
        public virtual Round Round { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public virtual TeamInMatchStats TeamInMatchStats { get; set; }
        public virtual IEnumerable<Activity> Activities { get; set; }
        public virtual IEnumerable<InteractionBetweenPlayers> InteractionBetweenPlayers { get; set; }
        [ForeignKey("TeamId1")]
        public virtual Team MatchTeam1 { get; set; }
        [ForeignKey("TeamId2")]
        public virtual Team MatchTeam2 { get; set; }
    }
}
