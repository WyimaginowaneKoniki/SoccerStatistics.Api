using System;
using System.Collections.Generic;

namespace SoccerStatistics.Api.Core.DTO
{
    public class MatchDTO
    {
        public uint Id { get; set; }
        public StadiumBasicDTO Stadium { get; set; }
        public uint AmountOfFans { get; set; }
        public RoundBasicDTO Round { get; set; }
        public DateTime Date { get; set; }
        public TeamInMatchStatsDTO TeamInMatchStats1 { get; set; }
        public TeamInMatchStatsDTO TeamInMatchStats2 { get; set; }
        public IEnumerable<ActivityDTO> Activities { get; set; }
        public IEnumerable<InteractionBetweenPlayersDTO> InteractionsBetweenPlayers { get; set; }
        public IEnumerable<ExtraTimeDTO> ExtraTime { get; set; }
        public OvertimeDTO Overtime { get; set; }
    }
}