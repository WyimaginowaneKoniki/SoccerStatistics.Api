using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Match
    {
        public uint Id { get; set; }
        [Required]
        public Stadium Stadium { get; set; }
        public uint AmountOfFans { get; set; }
        [Required]
        public Round Round { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public virtual IEnumerable<Activity> Activities { get; set; }
        public virtual IEnumerable<InteractionBetweenPlayers> InteractionsBetweenPlayers { get; set; }
        [Required]
        public TeamInMatchStats Team1 { get; set; }
        [Required]
        public TeamInMatchStats Team2 { get; set; }
    }
}
