using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerStatistics.Api.Core.DTO
{
    public class TeamInMatchStatsDTO
    {
        public uint Id { get; set; }
        public uint Pass { get; set; }
        public uint PassSuccess { get; set; }
        public uint BallPossesion { get; set; }
        public string Formation { get; set; }
        public TeamDTO Team { get; set; }
    }
}
