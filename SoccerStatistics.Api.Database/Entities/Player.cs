using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public uint Height { get; set; }
        public uint Weight { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public string Nationality { get; set; }
        public string DominantLeg { get; set; }
        public string Nick { get; set; }
        public uint Number { get; set; }

        [ForeignKey("Team")]
        public virtual Team Team { get; set; }
        [ForeignKey("Activities")]
        public virtual ICollection<Activity> Activities { get; set; }
        [ForeignKey("Interactions_between_players")]
        public virtual ICollection<InteractionBetweenPlayers> InteractionsBetweenPlayers { get; set; }
    }
}
