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
        public virtual Match Match { get; set; }
        [Required]
        [ForeignKey("InteractionsBetweenPlayer1")]
        public virtual Player Player1 { get; set; }
        [Required]
        [ForeignKey("InteractionsBetweenPlayer2")]
        public virtual Player Player2 { get; set; }

    }

}
