using System.Collections.Generic;

namespace SoccerStatistics.Api.Database.Entities
{
    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Shortname { get; set; }
        public string Season { get; set; }
        public string MVP { get; set; }
        public string Country { get; set; }
        public string Winner { get; set; }
        public virtual IEnumerable<Round> Rounds { get; set; }
        public virtual IEnumerable<Team> Teams { get; set; }
    }
}
