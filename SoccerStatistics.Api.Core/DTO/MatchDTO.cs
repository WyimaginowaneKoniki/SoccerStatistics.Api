using System;
using System.Collections.Generic;

namespace SoccerStatistics.Api.Core.DTO
{
    public class MatchDTO
    {
        public uint Id { get; set; }
        public StadiumDTO Stadium { get; set; }
        public uint AmountOfFans { get; set; }
        public RoundDTO Round { get; set; }
        public DateTime Date { get; set; }
        public TeamInMatchStatsDTO TeamInMatchStats1 { get; set; }
        public TeamInMatchStatsDTO TeamInMatchStats2 { get; set; }
        public virtual IEnumerable<ActivityDTO> Activities { get; set; }
        public virtual IEnumerable<InteractionBetweenPlayersDTO> InteractionsBetweenPlayers { get; set; }
    }
}