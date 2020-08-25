using SoccerStatistics.Api.Database.Validations;
using System.ComponentModel.DataAnnotations;

namespace SoccerStatistics.Api.Database.Entities
{
    public class TeamInMatchStats
    {
        public uint Id { get; set; }
        [Required]
        public uint Pass { get; set; }
        [Required]
        public uint PassSuccess { get; set; }
        [Required]
        public uint BallPossesion { get; set; }
        [Required]
        [StringLength(20)]
        [Formation]
        public string Formation { get; set; }
        [Required]
        public Team Team { get; set; }
    }
}
