using Microsoft.AspNetCore.Mvc;
using MyProfile.Services.Client.LeetCode;

namespace MyProfile.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeetCodeController : Controller
{
    private readonly ILogger<LeetCodeController> _logger;
    private readonly ILeetCodeClient _leetCodeClient;
    
    public LeetCodeController(ILogger<LeetCodeController> logger, ILeetCodeClient leetCodeClient)
    {
        _logger = logger;
        _leetCodeClient = leetCodeClient;
        
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(LeetCodeUserInfoResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken token = default)
    {
        _logger.LogInformation("Start processing GetUserInfo to leetcode");
        try
        {
            var result = await _leetCodeClient.GetUserInfo(token);
            return result is null or { Status: "error" } ? NotFound() : Ok(result);
        }
        catch (OperationCanceledException e)
        {
            _logger.LogInformation("Cant fetch data from leetcode");
            return Problem("Operation canceled by client : " + e.Message);
        }
        
    }
}