namespace SoccerStatistics.Api.Database.Entities
{
    public class Activity
    {
        public uint Id { get; set; }
        public ActivityType ActivityType { get; set; } //enum?
        public uint TimeAt { get; set; } 
        public string Description { get; set; }
        public virtual Player Player { get; set; }
        public virtual Match Match { get; set; }
    }
    
}
