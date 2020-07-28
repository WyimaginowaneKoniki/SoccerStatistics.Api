namespace SoccerStatistics.Api.Core.DTO
{
    public class TeamBasicDTO
    {
        public uint Id { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string Coach { get; set; }
        public string City { get; set; }
    }
}
