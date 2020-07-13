using SoccerStatistics.Api.Database.Entities;

namespace SoccerStatistics.Api.Core.DTO
{
    class ActivityDTO
    {
        public ActivityType ActivityType { get; set; }
        public uint TimeAt { get; set; }
        public string Description { get; set; }
        public uint PlayerId { get; set; }
        public uint MatchId { get; set; }
    }
}