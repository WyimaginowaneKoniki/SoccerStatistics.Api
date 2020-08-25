using SoccerStatistics.Api.Database.Entities;

namespace SoccerStatistics.Api.Core.DTO
{
    public class ActivityDTO
    {
        public ActivityType ActivityType { get; set; }
        public uint TimeAt { get; set; }
        public string Description { get; set; }
        public PlayerBasicDTO Player { get; set; }
    }
}