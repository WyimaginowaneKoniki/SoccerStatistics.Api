using System.ComponentModel.DataAnnotations;

namespace SoccerStatistics.Api.Database.Entities
{
    public class TeamInLeague
    {
        public uint Id { get; set; }
        [Required]
        public League League { get; set; }
        [Required]
        public Team Team { get; set; }
    }
}
