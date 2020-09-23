namespace SoccerStatistics.Api.Core.DTO
{
    public class FormationDTO
    {
        public PlayerBasicDTO Player { get; set; }
        // Unique player's number in formation
        /*  e.g. "4-4-2" where 0 == goalkeeper
         *    9 10
         *  5 6 7 8
         *  1 2 3 4
         *     0
         */
        public uint PositionNumber { get; set; }
    }
}
