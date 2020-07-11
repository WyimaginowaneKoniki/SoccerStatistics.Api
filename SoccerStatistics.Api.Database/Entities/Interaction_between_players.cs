using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Interaction_between_players
    {
        public int Id { get; set; }
        public string Interaction_type { get; set; } //enum?

        [DataType(DataType.Time)]
        public DateTime Time_at { get; set; } 
        public string Description { get; set; }
        [ForeignKey("Match")]
        public int Match_id { get; set; }
        public virtual Match Match { get; set; }
        [ForeignKey("Player1")]
        public virtual Player Player1 { get; set; }
        [ForeignKey("Player2")]
        public virtual Player Player2 { get; set; }

    }
}
