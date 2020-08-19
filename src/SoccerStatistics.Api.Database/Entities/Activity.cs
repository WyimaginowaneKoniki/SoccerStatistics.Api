using System.ComponentModel.DataAnnotations;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Activity
    {
        public uint Id { get; set; }
        [Required]
        public ActivityType ActivityType { get; set; }
        [Required]
        public uint TimeAt { get; set; } 
        [StringLength(50)]
        public string Description { get; set; }
        [Required]
        public Player Player { get; set; }
    }    
}
