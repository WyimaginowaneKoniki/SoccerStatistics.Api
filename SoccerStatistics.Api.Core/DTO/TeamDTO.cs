using System;
using System.Collections.Generic;

namespace SoccerStatistics.Api.Core.DTO
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Coach { get; set; }
        public string City { get; set; }
        public virtual IEnumerable<PlayerDTO> Players { get; set; }
        public virtual StadiumDTO Stadium { get; set; }//change to ID
        public virtual LeagueDTO League { get; set; }//change to ID

    }
}
