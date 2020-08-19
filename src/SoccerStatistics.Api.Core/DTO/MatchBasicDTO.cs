using System;

namespace SoccerStatistics.Api.Core.DTO
{
    public class MatchBasicDTO
    {
        public uint Id { get; set; }
        public string Result { get; set; }
        public DateTime Date { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
    }
}
