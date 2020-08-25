using System.ComponentModel.DataAnnotations;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Stadium
    {
        public uint Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Country { get; set; }
        [Required]
        [StringLength(50)]
        public string City { get; set; }
        public uint BuiltAt { get; set; }
        [Required]
        public uint Capacity { get; set; }
        [Required]
        [StringLength(20)]
        public string FieldSize { get; set; }
        public decimal Cost { get; set; }
        [Required]
        public uint VipCapacity { get; set; }
        [Required]
        public bool IsForDisabled { get; set; }
        public uint Lighting { get; set; }
        [StringLength(50)]
        public string Architect { get; set; }
        [Required]
        public bool IsNational { get; set; }
    }
}
