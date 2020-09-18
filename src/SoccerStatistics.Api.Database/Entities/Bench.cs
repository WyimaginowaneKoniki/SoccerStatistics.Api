using System.ComponentModel.DataAnnotations;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Bench
    {
        public uint Id { get; set; }
        [Required]
        public Player Player { get; set; }
    }
}
