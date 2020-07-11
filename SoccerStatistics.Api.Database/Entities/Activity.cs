using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Activity
    {
        public int Id { get; set; }
        public string Activity_type { get; set; } //enum?
        public string Time_At { get; set; } //datetime?
        public string Description { get; set; }
        [ForeignKey("Player")]
        public int Player_id { get; set; }
        public virtual Player Player { get; set; }
        [ForeignKey("Match")]
        public int Match_id { get; set; }
        public virtual Match Match { get; set; }
    }
}
