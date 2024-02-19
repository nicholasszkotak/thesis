using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thesis_exercise.Services.Models
{
    public record ComputerModel(
        Guid? ID,
        int RAM,
        int DiskSpace,
        string DiskType,
        string GraphicsCard,
        decimal Weight,
        int Power,
        string Processor,
        List<PortModel> Ports
    );
}
