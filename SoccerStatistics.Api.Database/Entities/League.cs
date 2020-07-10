using System.Collections.Generic;

namespace SoccerStatistics.Database.Entities
{
    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Season { get; set; }
        public string MVP { get; set; }
        public string Country { get; set; }
        public string Winner { get; set; }
        public virtual ICollection<Round> Rounds { get; set; }
    }
}
