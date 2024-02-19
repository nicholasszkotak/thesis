using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thesis_exercise.DataAccess.Dtos
{
    public class PortData
    {
        public Guid? Id {get; set;}
        public Guid ComputerId {get; set;}
        public string Type {get; set;}
        public int Quantity {get; set;}
    }
}