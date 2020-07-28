using SoccerStatistics.Api.Database.Entities;

namespace SoccerStatistics.Api.Core.DTO
{
    public class InteractionBetweenPlayersDTO
    {
        public InteractionType InteractionType { get; set; }
        public uint TimeAt { get; set; }
        public string Description { get; set; }
        public PlayerBasicDTO Player1 { get; set; }
        public PlayerBasicDTO Player2 { get; set; }
    }
}