using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Team
    {
        public uint Id { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        [StringLength(50)]
        public string ShortName { get; set; }
        public uint CreatedAt { get; set; }
        [Required]
        [StringLength(50)]
        public string Coach { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [Required]
        public IEnumerable<Player> Players { get; set; }
        public Stadium Stadium { get; set; }
    }
}
