using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Activity
    {
        public int Id { get; set; }
        public string Activity_type { get; set; } //enum?
        [DataType(DataType.Time)]
        public DateTime Time_At { get; set; } 
        public string Description { get; set; }
        [ForeignKey("Player")]
        public int Player_id { get; set; }
        public virtual Player Player { get; set; }
        [ForeignKey("Match")]
        public int Match_id { get; set; }
        public virtual Match Match { get; set; }
    }
}
