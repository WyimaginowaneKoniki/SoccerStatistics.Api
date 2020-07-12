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
        public virtual  Stadium Stadium { get; set; }
        public virtual League League { get; set; }
        [InverseProperty("DestTeam")]
        public virtual IEnumerable<Transfer> Transfer1 { get; set; }
        [InverseProperty("SourceTeam")]
        public virtual IEnumerable<Transfer> Transfer2 { get; set; }
        [InverseProperty("MatchTeam1")]
        public virtual IEnumerable<Match> TeamId1 { get; set; }
        [InverseProperty("MatchTeam2")]
        public virtual IEnumerable<Match> TeamId2 { get; set; }
    }
}
