using System.Collections.Generic;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Round
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("League")]
        public int League_id { get; set; }
        public virtual League League { get; set; }
        [ForeignKey("Round")]
        public int Round_id { get; set; }
        public virtual ICollection<Round> Rounds { get; set; }
    }
}
