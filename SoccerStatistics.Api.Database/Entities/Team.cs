using System;
using System.Collections.Generic;

namespace SoccerStatistics.Database.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public DateTime Created_At { get; set; }
        public string Coach { get; set; }
        public string City { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        [ForeignKey("Stadium")]
        public int Stadium_id { get; set; }
        public virtual  Stadium Stadium { get; set; }
    }
}
