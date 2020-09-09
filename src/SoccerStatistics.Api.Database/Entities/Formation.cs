using SoccerStatistics.Api.Database.Validations;
using System.ComponentModel.DataAnnotations;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Formation
    {
        public uint Id { get; set; }
        [Required]
        public TeamInMatchStats TeamInMatchStats { get; set; }
        [Required]
        public Player Player { get; set; }
        [Required]
        [PositionInFormation]
        public uint PositionNumber { get; set; }
    }
}
