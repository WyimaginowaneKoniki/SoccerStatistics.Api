using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Transfer
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public PlayerPosition PlayerPosition { get; set; }
        [ForeignKey("Player")]
        public virtual Player Player { get; set; }
        [ForeignKey("DestTeam")]
        public virtual Team DestTeam { get; set; }
        [ForeignKey("SourceTeam")]
        public virtual Team SourceTeam { get; set; }

    }
}
