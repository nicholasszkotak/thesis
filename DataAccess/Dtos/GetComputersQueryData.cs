using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thesis_exercise.DataAccess.Dtos
{
    public class GetComputersQueryData
    {
        public List<ComputerData> Computers {get; set;}
        public List<PortData> Ports {get; set;}
    }
}