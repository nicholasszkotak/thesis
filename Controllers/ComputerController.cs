using Microsoft.AspNetCore.Mvc;
using thesis_exercise.Controllers.Requests;
using thesis_exercise.Services;
using thesis_exercise.Services.Models;

namespace thesis_exercise.Controllers;

[ApiController]
[Route("[controller]")]
public class ComputerController : ControllerBase
{

    private readonly IComputerService computerService;

    public ComputerController(IComputerService computerService)
    {
        this.computerService = computerService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var computers = await computerService.GetComputers();
        return new JsonResult(computers);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] AddComputerRequest request) {
      var newId = await computerService.AddComputer(new ComputerModel(
            null,
            request.RAM,
            request.DiskSpace,
            request.DiskType,
            request.GraphicsCard,
            request.Weight,
            request.Power,
            request.Processor,
            request.Ports
        ));

        return new JsonResult(newId);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UpdateComputerRequest request) {

        await computerService.UpdateComputer(new ComputerModel(
            request.Id,
            request.RAM,
            request.DiskSpace,
            request.DiskType,
            request.GraphicsCard,
            request.Weight,
            request.Power,
            request.Processor,
            request.Ports
        ));

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid id) {
        await computerService.DeleteComputer(id);
        return Ok();
    }

    [HttpPost]
    [Route("Search")]
    public async Task<IActionResult> Search([FromBody] SearchRequest request)
    {
        var computers = await computerService.SearchComputers(new SearchComputersModel(
            request.RAM,
            request.DiskSpace,
            request.DiskType,
            request.GraphicsCard,
            request.Weight,
            request.Power,
            request.Processor
        ));

        return new JsonResult(computers);
    }
}
