using System;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Transfer
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Player")]
        public int Player_id { get; set; }
        public virtual Player Player { get; set; }
        [ForeignKey("Dest_team")]
        public virtual Team Dest_team { get; set; }
        [ForeignKey("Source_team")]
        public virtual Team Source_team { get; set; }

    }
}
