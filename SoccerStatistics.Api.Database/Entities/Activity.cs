using System.ComponentModel.DataAnnotations;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Activity
    {
        public uint Id { get; set; }
        public ActivityType ActivityType { get; set; } 
        public uint TimeAt { get; set; } 
        [StringLength(50)]
        public string Description { get; set; }
        public virtual Player Player { get; set; }
        public virtual Match Match { get; set; }
    }
    
}
