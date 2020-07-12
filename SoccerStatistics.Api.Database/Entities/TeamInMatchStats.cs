using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerStatistics.Api.Database.Entities
{
    public class TeamInMatchStats
    {
        public uint Id { get; set; }
        public uint Pass { get; set; }
        public uint PassSuccess { get; set; }
        public uint BallPossesion { get; set; }
        public string Formation { get; set; }
        [ForeignKey("MatchId")]
        public virtual Match Match { get; set; }
        public virtual Team Team { get; set; }

    }
}
