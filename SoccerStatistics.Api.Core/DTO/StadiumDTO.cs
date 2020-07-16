﻿using System;
using System.Collections.Generic;
using System.Text;

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
        public decimal Cost { get; set; }
        public uint VipCapacity { get; set; }
        public bool IsForDisabled { get; set; }
        public uint Lighting { get; set; }
        public string Architect { get; set; }
        public bool IsNational { get; set; }
        public TeamDTO Team { get; set; }
    }
}