using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public TeamInMatchStats TeamOneStats { get; set; }
        [Required]
        public TeamInMatchStats TeamTwoStats { get; set; }
    }
}
