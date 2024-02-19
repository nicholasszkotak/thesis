using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thesis_exercise.Services.Models
{
    public record PortModel(
        Guid? Id,
        Guid ComputerId,
        string Type,
        int Quantity
    );
}
