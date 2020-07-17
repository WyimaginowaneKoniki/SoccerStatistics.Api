using System;
using System.Collections.Generic;

namespace SoccerStatistics.Api.Core.DTO
{
    public class MatchDTO
    {
        public uint StadiumId { get; set; }
        public uint AmountOfFans { get; set; }
        public uint RoundId { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<ActivityDTO> Activities { get; set; }
        public IEnumerable<InteractionBetweenPlayersDTO> InteractionBetweenPlayers { get; set; }
        public uint MatchTeam1Id { get; set; }
        public uint MatchTeam2Id { get; set; }
    }
}