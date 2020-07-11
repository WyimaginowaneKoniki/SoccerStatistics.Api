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
        public int Height { get; set; }
        public int Weight { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public string Nationality { get; set; }
        public string Dominant_leg { get; set; }
        public string Nick { get; set; }
        
        public int Number { get; set; }
        public string Player_position { get; set; }
        [ForeignKey("Team")]
        public int Team_id { get; set; }
        public virtual Team Team { get; set; }
        [ForeignKey("Activities")]
        public int Activity_id { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        [ForeignKey("Interactions_between_players")]
        public int Interactions_between_players_id { get; set; }
        public virtual ICollection<Interaction_between_players> Interactions_between_players { get; set; }
    }
}
