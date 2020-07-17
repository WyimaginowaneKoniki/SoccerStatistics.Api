using System.Collections.Generic;

namespace SoccerStatistics.Api.Core.DTO
{
    public class RoundDTO
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public LeagueBasicDTO League { get; set; }
        public IEnumerable<MatchDTO> Matches { get; set; }
    }
}
