namespace SoccerStatistics.Api.Core.DTO
{
    public class PenaltyKickDTO
    {
        public PlayerBasicDTO Shooter { get; set; }
        public PlayerBasicDTO Goalkeeper { get; set; }
        public bool IsGoal { get; set; }
    }
}