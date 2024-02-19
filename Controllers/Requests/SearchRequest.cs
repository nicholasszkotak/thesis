using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thesis_exercise.Controllers.Requests
{
    public class SearchRequest
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