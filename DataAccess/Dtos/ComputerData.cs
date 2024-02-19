using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thesis_exercise.DataAccess.Dtos
{
    public class ComputerData
    {
        public Guid? Id {get; set;}
        public int RAM {get; set;}
        public int DiskSpace {get; set;}
        public string DiskType {get; set;}
        public string GraphicsCard {get; set;}
        public decimal Weight {get; set;}
        public int Power {get; set;}
        public string Processor {get; set;}
        public List<PortData> Ports {get; set;}
    }
}