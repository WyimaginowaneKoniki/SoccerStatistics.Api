using System;

namespace SoccerStatistics.Api.Core.DTO
{
    public class PlayerDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public uint Height { get; set; }
        public uint Weight { get; set; }
        public DateTime Birthday { get; set; }
        public uint Age { get; set; }
        public string Nationality { get; set; }
        public string DominantLeg { get; set; }
        public string Nick { get; set; }
        public uint Number { get; set; }
        public uint TeamId { get; set; }
    }
}
