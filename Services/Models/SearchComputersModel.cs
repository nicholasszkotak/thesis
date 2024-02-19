using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace thesis_exercise.Services.Models
{
    public record SearchComputersModel(
        string? RAM,
        string? DiskSpace,
        string? DiskType,
        string? GraphicsCard,
        string? Weight,
        string? Power,
        string? Processor
    );
}
