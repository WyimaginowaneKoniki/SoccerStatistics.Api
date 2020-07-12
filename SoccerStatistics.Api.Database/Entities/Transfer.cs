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
        public virtual Player Player { get; set; }
        [ForeignKey("Transfer1")]
        public virtual Team DestTeam { get; set; }
        [ForeignKey("Transfer2")]
        public virtual Team SourceTeam { get; set; }
   

    }
}
