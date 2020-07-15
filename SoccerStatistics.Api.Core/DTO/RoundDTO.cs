using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerStatistics.Api.Core.DTO
{
    public class RoundDTO
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public virtual uint LeagueId { get; set; }
        public virtual IEnumerable<MatchDTO> Matches { get; set; }
    }
}
