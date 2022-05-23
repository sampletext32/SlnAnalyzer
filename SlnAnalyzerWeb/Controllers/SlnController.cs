using Microsoft.AspNetCore.Mvc;
using SlnAnalyzerWeb.Services;

namespace SlnAnalyzerWeb.Controllers;

[Controller]
[Route("[controller]/[action]")]
public class SlnController : Controller
{
    private ISlnService _slnService;

    public SlnController(ISlnService slnService)
    {
        _slnService = slnService;
    }

    [HttpGet]
    public async Task<ActionResult> OpenSln(string? path)
    {
        return Ok(_slnService.OpenSln(path));
    }

    [HttpGet]
    public async Task<ActionResult> OpenCsproj(string slnPath, string csprojPath)
    {
        return Ok(_slnService.OpenCsproj(slnPath, csprojPath));
    }
}