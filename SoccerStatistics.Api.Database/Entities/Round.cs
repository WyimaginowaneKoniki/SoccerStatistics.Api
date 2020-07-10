namespace SoccerStatistics.Database.Entities
{
    public class Round
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("League")]
        public int League_id { get; set; }
        public League League { get; set; }
    }
}
