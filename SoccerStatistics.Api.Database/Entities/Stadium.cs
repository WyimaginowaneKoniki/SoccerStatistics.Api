using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Stadium
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        [DataType(DataType.Date)]
        public DateTime BuiltAt { get; set; }
        public uint Capacity { get; set; }
        public string FieldSize { get; set; }
        public decimal Cost { get; set; }
        public uint VipCapacity { get; set; }
        public bool IsForDisabled { get; set; }
        public uint Lighting { get; set; } 
        public string Architect { get; set; }
        public bool IsNational { get; set; }
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
