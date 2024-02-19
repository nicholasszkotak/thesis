using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thesis_exercise.DataAccess.Dtos
{
    public class SearchComputersData
    {
        public string? RAM {get; set;}
        public string? DiskSpace {get; set;}
        public string? DiskType {get; set;}
        public string? GraphicsCard {get; set;}
        public string? Weight {get; set;}
        public string? Power {get; set;}
        public string? Processor {get; set;}
    }
}