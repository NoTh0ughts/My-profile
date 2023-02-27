using Microsoft.EntityFrameworkCore;
using Data;
using Data.Models;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using MyProfile.Services.Client.Github;
using MyProfile.Services.Timed_Worker.Extensions;

namespace MyProfile.Services.Timed_Worker;

public interface IScopedProjectUpdaterService
{
    Task DoWork(CancellationToken cancellationToken);
}

public class ScopedProjectsUpdaterService : IScopedProjectUpdaterService
{
    private int _executionCount = 0;
    
    private readonly ILogger<ScopedProjectsUpdaterService> _logger;
    private readonly IOptionsMonitor<UserConfiguration> _config;
    
    private readonly MyProjectsContext _dbContext;
    
    private readonly IGithubResourceClient _resourceClient;
    private readonly IGithubUserClient _userClient;

    public ScopedProjectsUpdaterService(ILogger<ScopedProjectsUpdaterService> logger, 
        IOptionsMonitor<UserConfiguration> config,
        MyProjectsContext dbContext,
        IGithubResourceClient resourceClient,
        IGithubUserClient userClient)
    {
        _logger = logger;
        _config = config;
        _dbContext = dbContext;
        _resourceClient = resourceClient;
        _userClient = userClient;
    }

    /// <summary> The DoWork function is the method that will be called when the service starts.
    /// It does not take any parameters and returns a Task.</summary>
    ///
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    ///
    /// <returns> A &lt;see cref=&quot;task{tresult}&quot;/&gt; that represents the asynchronous operation.</returns>
    public async Task DoWork(CancellationToken cancellationToken)
    {
        while (cancellationToken.IsCancellationRequested == false)
        {
            _executionCount++;
            _logger.LogInformation("Scoped Project Updater Service is working. Count: {Count}", _executionCount);

            // Загружаем инфу о проектах
            var projects = await FetchProjects();
            
            try
            {
                foreach (var project in projects)
                {
                    _dbContext.Entry(project).State =_dbContext.Projects.Any(x => x.ID == project.ID) ?
                        EntityState.Modified :
                        EntityState.Added;
                    
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    
                    _dbContext.Entry(project).State = EntityState.Detached;
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to update user repos, error: {}", e.Message);
            }
            _logger.LogDebug("Count now : {Count}", _dbContext.Projects.Count());

            var peridodicity = (int)_config.CurrentValue.UpdatePeriodicity.TotalMilliseconds;
            _logger.LogDebug("{Periodicity}", peridodicity.ToString());
            await Task.Delay(peridodicity, cancellationToken);
        }
    }

    /// <summary> The FetchProjects function fetches the projects from the GitHub API and saves them to a database.</summary>
    ///
    ///
    /// <returns> A list of projects.</returns>
    private async Task<IEnumerable<Project>> FetchProjects()
    {
        // Получаем список репозиториев пользователя
        var userRepos = await _userClient.GetUserRepositories();
        // Тут будет список проектов
        var projectsList = new List<Project>(userRepos.Count);
        
        // Тут мы собираем данные в одну запись проекта и добавляем ее, если нормально все будет
        foreach (var repo in userRepos)
        {
            try
            {
                // Достаем инфу о репозитории и парсим ее
                var stringRepoInfo = await _resourceClient.GetRepositoryInfo(repo.Name);
                var repoInfo = ParseFromHtml(stringRepoInfo);

                // Находим технологии которые уже есть
                var prTechs = _dbContext.Technologies
                    .Where(t => repoInfo.Technologies.Contains(t.Name))
                    .AsNoTrackingWithIdentityResolution()
                    .ToList();
    
                // И которых нет
                foreach (var technology in repoInfo.Technologies
                             .Where(x => prTechs.Select(tn => tn.Name).Contains(x) == false)
                             .Select(x => new Technology {Name = x}))
                {
                    prTechs.Add(technology);
                    _dbContext.Entry(technology).State = EntityState.Added;
                }
                
                await _dbContext.SaveChangesAsync();
                
                var newProject = new Project
                {
                    ID             = repo.Id,
                    CreatedAt      = repo.CreatedAt,
                    UpdatedAt      = repo.UpdatedAt,
                    RepositoryName = repo.Name,
                    MainTitle      = repoInfo.MainTitle,
                    SubTitle       = repoInfo.SubTitle,
                    Technologies   = prTechs,
                };
                
                projectsList.Add(newProject);
            }
            catch (Exception e)
            {
                _logger.LogDebug("Unable to update user repos, error: {Error}, with repo: {RepoName}", 
                    e.Message, repo.Name);
            }
        }

        return projectsList;
    }

    private RepositoryInfoResponse ParseFromHtml(string htmlString)
    {
        HtmlDocument html = new HtmlDocument();
        html.LoadHtml(htmlString);

        var repoInfo = new RepositoryInfoResponse
        {
            MainTitle = html.GetElementSingleValue("MainTitle"),
            SubTitle = html.GetElementSingleValue("SubTitle"),
            Technologies = html.GetElementListValue("TechStack", '*').ToArray()
        };

        return repoInfo;
    }
}