using SoccerStatistics.Api.Database.Validations;
using System.Collections.Generic;
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
        [Range(10, 10)]
        public IEnumerable<Formation> PlayersInFormation { get; set; }
        [Required]
        public IEnumerable<Bench> PlayersOnBench { get; set; }
        [Required]
        public Team Team { get; set; }
    }
}
