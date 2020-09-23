using System.Collections.Generic;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Overtime
    {
        public uint Id { get; set; }
        public IEnumerable<ExtraTime> ExtraTime { get; set; }
        public IEnumerable<PenaltyKick> PenaltyKicks { get; set; }
    }
}