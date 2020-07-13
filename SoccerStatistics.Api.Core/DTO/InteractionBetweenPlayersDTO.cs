using SoccerStatistics.Api.Database.Entities;

namespace SoccerStatistics.Api.Core.DTO
{
    class InteractionBetweenPlayersDTO
    {
        public InteractionType InteractionType { get; set; }
        public uint TimeAt { get; set; }
        public string Description { get; set; }
        public uint MatchId { get; set; }
        public uint Player1Id { get; set; }
        public uint Player2Id { get; set; }
    }
}