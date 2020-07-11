using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerStatistics.Api.Database.Entities
{
    public class InteractionBetweenPlayers
    {
        public int Id { get; set; }
        public InteractionType InteractionType { get; set; }

        public uint TimeAt { get; set; } 
        public string Description { get; set; }
        [ForeignKey("Match")]
        public virtual Match Match { get; set; }
        [ForeignKey("Player1")]
        public virtual Player Player1 { get; set; }
        [ForeignKey("Player2")]
        public virtual Player Player2 { get; set; }

    }

}
