using System.ComponentModel.DataAnnotations;

namespace SoccerStatistics.Api.Database.Entities
{
    public class InteractionBetweenPlayers
    {
        public uint Id { get; set; }
        [Required]
        public InteractionType InteractionType { get; set; }
        [Required]
        public uint TimeAt { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        public Player Player1 { get; set; }
        [Required]
        public Player Player2 { get; set; }
    }
}