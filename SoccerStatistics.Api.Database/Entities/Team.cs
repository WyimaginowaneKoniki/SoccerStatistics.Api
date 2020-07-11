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
        public DateTime Created_At { get; set; }
        public string Coach { get; set; }
        public string City { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        [ForeignKey("Stadium")]
        public int Stadium_id { get; set; }
        public virtual  Stadium Stadium { get; set; }
        public virtual ICollection<Transfer> Transfers { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
    }
}
