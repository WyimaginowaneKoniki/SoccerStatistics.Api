using System;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public DateTime Birthday { get; set; }
        public string Nationality { get; set; }
        public string Dominant_leg { get; set; }
        public string Nick { get; set; }
        
        public int Number { get; set; }
        public string Player_position { get; set; }
        [ForeignKey("Team")]
        public int Team_id { get; set; }
        public virtual Team Team { get; set; }
    }
}
