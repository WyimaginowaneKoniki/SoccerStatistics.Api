using System;

namespace SoccerStatistics.Database.Entities
{
    public class Stadium
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime Built_At { get; set; }
        public int Capacity { get; set; }
        public string Field_size { get; set; }
        public int Cost { get; set; }
        public int Vip_Capacity { get; set; }
        public bool Is_For_Disabled { get; set; }
        public int Lighting { get; set; } 
        public string Architect { get; set; }
        public bool Is_National { get; set; }
        [ForeignKey("Team")]
        public int Team_id { get; set; }
        public Team Team { get; set; }
    }
}
