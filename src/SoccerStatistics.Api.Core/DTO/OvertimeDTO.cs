using System.Collections.Generic;

namespace SoccerStatistics.Api.Core.DTO
{
    public class OvertimeDTO
    {
        public IEnumerable<ExtraTimeDTO> ExtraTime { get; set; }
        public IEnumerable<PenaltyKickDTO> PenaltyKicks { get; set; }
    }
}
