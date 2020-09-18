using System.Collections.Generic;

namespace SoccerStatistics.Api.Core.DTO
{
    public class TeamInMatchStatsDTO
    {
        public uint Id { get; set; }
        public TeamBasicDTO Team { get; set; }
        public uint Pass { get; set; }
        public uint PassSuccess { get; set; }
        public uint PassSuccessPercentage { get; set; }
        public uint BallPossesion { get; set; }
        public string Formation { get; set; }
        public uint RedCards { get; set; }
        public uint YellowCards { get; set; }
        public uint Fouls { get; set; }
        public IEnumerable<PlayerBasicDTO> PlayersOnBench { get; set; }
        public IEnumerable<FormationDTO> PlayersInFormation { get; set; }
        public uint CornerKicks { get; set; }
        public uint PenaltyKicks { get; set; }
        public uint FreeKicks { get; set; }
        public uint Shots { get; set; }
        public uint ShotsOnGoal { get; set; }
        public uint ShotsOnGoalPercentage { get; set; }
        public IEnumerable<InteractionBetweenPlayersDTO> Goals { get; set; }
        public IEnumerable<ActivityDTO> Injuries { get; set; }
        public IEnumerable<InteractionBetweenPlayersDTO> Substitutions { get; set; }
        public uint Offsides { get; set; }
        public uint Clearances { get; set; }
    }
}
