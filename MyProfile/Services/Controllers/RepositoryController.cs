using AutoMapper;
using Data;
using Data.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProfile.Services.Cache;

namespace MyProfile.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RepositoryController : Controller
{
    private readonly ILogger<RepositoryController> _logger;
    private readonly MyProjectsContext _context;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public RepositoryController(ILogger<RepositoryController> logger, 
        MyProjectsContext context, 
        IMapper mapper, 
        ICacheService cacheService)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
        _cacheService = cacheService;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status418ImATeapot)]
    [ProducesResponseType(typeof(ProjectDTO[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken token)
    {
        _logger.LogInformation("Context: {Id}", _context.ContextId.InstanceId);
        try
        {
            // Попытка получить данные из кэша
            var cachedProjects = await _cacheService.GetCachedDataAsync<IEnumerable<ProjectDTO>>("projects");
            if (cachedProjects != null)
            {
                _logger.LogInformation("Retrived projects data from cache");
                return Ok(cachedProjects);
            }

            // Если данных нет в кэше, выполняем запрос к базе данных
            var projects = await _context.Projects
                .Include(x => x.Technologies)
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(token);

            var projectDtos = _mapper.Map<IEnumerable<ProjectDTO>>(projects);

            // Кэшируем данные
#pragma warning disable CS4014
            _cacheService.SetCachedDataAsync("projects", projectDtos, TimeSpan.FromMinutes(5));
#pragma warning restore CS4014

            return Ok(projectDtos);
        }
        catch (OperationCanceledException e)
        {
            return Problem("Operation canceled by client: " + e.Message, statusCode: StatusCodes.Status418ImATeapot);
        }
        catch (ArgumentNullException e)
        {
            return Problem("Data source is null, internal server error: " + e.Message, statusCode: StatusCodes.Status418ImATeapot);
        }
    }
}