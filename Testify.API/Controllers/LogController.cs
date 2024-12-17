using Microsoft.AspNetCore.Mvc;
using Testify.DAL.Reposiroties;

namespace Testify.API.Controllers;

[ApiController]
[Route("LogUser")]
public class LogController:ControllerBase
{
    private readonly LogRepository _logRepository;

    public LogController( )
    {
        _logRepository = new LogRepository();
    }
    
    [HttpGet("by-guid")]
    public IActionResult GetLogsByGuid([FromQuery] string guid)
    {
        if (string.IsNullOrWhiteSpace(guid))
        {
            return BadRequest("GUID không được để trống.");
        }

        var logs = _logRepository.GetLogsByMessageGuid(guid);
        if (!logs.Any())
        {
            return NotFound("Không tìm thấy logs chứa GUID.");
        }

        return Ok(logs);
    }
}