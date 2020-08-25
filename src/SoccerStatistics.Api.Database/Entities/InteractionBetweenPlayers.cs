using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerStatistics.Api.Database.Entities
{
    public class InteractionBetweenPlayers
    {
        public uint Id { get; set; }
        [Required]
        public InteractionType InteractionType { get; set; }
        [Required]
        public uint TimeAt { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [Required]
        public Player Player1 { get; set; }
        [Required]
        public Player Player2 { get; set; }
    }
}