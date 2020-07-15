using System.Collections.Generic;

namespace SoccerStatistics.Api.Core.DTO
{
    public class RoundDTO
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public uint LeagueId { get; set; }
        public string LeagueName { get; set; }

        public virtual IEnumerable<MatchDTO> Matches { get; set; }


    }
}
