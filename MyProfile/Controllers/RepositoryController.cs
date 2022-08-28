using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyProfile.Controllers;

[ApiController]
[Route("[controller]")]
public class RepositoryController : Controller
{
    private readonly ILogger<RepositoryController> _logger;
    private readonly MyProjectsContext _context;
    
    public RepositoryController(ILogger<RepositoryController> logger, MyProjectsContext context)
    {
        _logger = logger;
        _context = context;
    }


    [HttpGet("idk")]
    public async Task<IActionResult> GetAllProjects(CancellationToken token)
    {
        try
        {
            var result = await _context.Projects
                .Include(x => x.Technologies)
                .AsNoTracking()
                .ToListAsync(token);

            return Ok(result);
        }
        catch (OperationCanceledException e)
        {
            return Problem("Operation canceled by client : " + e.Message);
        }
        catch (ArgumentNullException e)
        {
            return Problem("Data source is null, internal server error : " + e.Message);
        }
    }
}