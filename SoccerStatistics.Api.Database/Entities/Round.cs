using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Round
    {
        public uint Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        public virtual League League { get; set; }
        public virtual IEnumerable<Match> Matches { get; set; }
    }
}
