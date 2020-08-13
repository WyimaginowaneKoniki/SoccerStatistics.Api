using System;
using System.ComponentModel.DataAnnotations;

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
        public Player Player { get; set; }
        [Required]
        public Team DestTeam { get; set; }
        public Team SourceTeam { get; set; }
    }
}
