using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using thesis_exercise.Services.Models;

namespace thesis_exercise.Controllers.Requests
{
    public class AddComputerRequest
    {
        public int RAM {get; set;}
        public int DiskSpace {get; set;}
        public string DiskType {get; set;}
        public string GraphicsCard {get; set;}
        public decimal Weight {get; set;}
        public int Power {get; set;}
        public string Processor {get; set;}
        public List<PortModel> Ports {get; set;}
    }
}