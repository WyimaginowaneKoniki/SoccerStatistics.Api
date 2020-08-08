using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Formation { get; set; }
        [Required]
        public Team Team { get; set; }
    }
}
