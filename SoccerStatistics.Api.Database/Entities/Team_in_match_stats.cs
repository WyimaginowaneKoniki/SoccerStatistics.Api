namespace SoccerStatistics.Api.Database.Entities
{
    public class Team_in_match_stats
    {
        public int Id { get; set; }
        public int Pass { get; set; }
        public int Pass_Success { get; set; }
        public int Ball_possesion { get; set; }
        public string Formation { get; set; }
        [ForeignKey("Match")]
        public int Match_id { get; set; }
        public virtual Match Match { get; set; }
    }
}
