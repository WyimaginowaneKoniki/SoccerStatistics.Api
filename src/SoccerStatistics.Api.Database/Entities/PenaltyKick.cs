using System.ComponentModel.DataAnnotations;

namespace SoccerStatistics.Api.Database.Entities
{
    public class PenaltyKick
    {
        public uint Id { get; set; }
        [Required]
        public Player Shooter { get; set; }
        [Required]
        public Player Goalkeeper { get; set; }
        [Required]
        public bool IsGoal { get; set; }
    }
}
