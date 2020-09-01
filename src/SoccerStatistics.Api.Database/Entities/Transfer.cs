using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Transfer
    {
        public uint Id { get; set; }
        [Column(TypeName = "decimal(9, 0)")]
        public decimal Price { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }
        [Required]
        public PlayerPosition PlayerPosition { get; set; }
        [Required]
        public Player Player { get; set; }
        [Required]
        public Team DestTeam { get; set; }
        public Team SourceTeam { get; set; }
    }
}
