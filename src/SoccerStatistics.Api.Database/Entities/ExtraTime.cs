using System.ComponentModel.DataAnnotations;

namespace SoccerStatistics.Api.Database.Entities
{
    public class ExtraTime
    {
        public uint Id { get; set; }
        [Required]
        public uint AdditionalTime { get; set; }
        [Required]
        public uint TimeAt { get; set; }
    }
}
