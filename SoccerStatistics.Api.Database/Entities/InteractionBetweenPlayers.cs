using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerStatistics.Api.Database.Entities
{
    public class InteractionBetweenPlayers
    {
        public uint Id { get; set; }
        public InteractionType InteractionType { get; set; }
        public uint TimeAt { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public virtual Match Match { get; set; }
        [ForeignKey("InteractionsBetweenPlayer1")]
        public virtual Player Player1 { get; set; }
        [ForeignKey("InteractionsBetweenPlayer2")]
        public virtual Player Player2 { get; set; }

    }

}
