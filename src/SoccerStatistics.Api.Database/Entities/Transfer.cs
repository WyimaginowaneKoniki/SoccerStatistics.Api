using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Transfer
    {
        public uint Id { get; set; }
        public decimal Price { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public PlayerPosition PlayerPosition { get; set; }
        [Required]
        public virtual Player Player { get; set; }
        [Required]
        [ForeignKey("Transfer1")]
        public virtual Team DestTeam { get; set; }
        [ForeignKey("Transfer2")]
        public virtual Team SourceTeam { get; set; }


    }
}
