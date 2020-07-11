using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Activity
    {
        public int Id { get; set; }
        public ActivityType ActivityType { get; set; } //enum?
       
        public uint TimeAt { get; set; } 
        public string Description { get; set; }
        [ForeignKey("Player")]
        public virtual Player Player { get; set; }
        [ForeignKey("Match")]
        public virtual Match Match { get; set; }
    }
    
}
