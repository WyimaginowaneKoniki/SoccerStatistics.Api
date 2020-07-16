using System.Collections.Generic;

namespace SoccerStatistics.Api.Core.DTO
{
    public class LeagueDTO
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Shortname { get; set; }
        public string Season { get; set; }
        public string MVP { get; set; }
        public string Country { get; set; }
        public string Winner { get; set; }
        public IEnumerable<TeamBasicDTO> Teams { get; set; }
    }
}
