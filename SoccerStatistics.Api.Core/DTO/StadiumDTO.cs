namespace SoccerStatistics.Api.Core.DTO
{
    public class StadiumDTO
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public uint BuiltAt { get; set; }
        public uint Capacity { get; set; }
        public string FieldSize { get; set; }
        public string Cost { get; set; }
        public uint VipCapacity { get; set; }
        public bool IsForDisabled { get; set; }
        public uint Lighting { get; set; }
        public string Architect { get; set; }
        public bool IsNational { get; set; }
        public TeamBasicDTO Team { get; set; }
    }
}