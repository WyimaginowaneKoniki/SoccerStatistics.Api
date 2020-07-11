using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
        public string Coach { get; set; }
        public string City { get; set; }
        public virtual IEnumerable<Player> Players { get; set; }
        [ForeignKey("Stadium")]
        public int Stadium_id { get; set; }
        public virtual  Stadium Stadium { get; set; }
        [ForeignKey("League")]
        public virtual League League { get; set; }
        public virtual IEnumerable<Transfer> Transfers { get; set; }
        public virtual IEnumerable<Match> Matches { get; set; }
    }
}
