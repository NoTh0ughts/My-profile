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
    
    private readonly MyProjectsContext _ctx;
    
    private readonly IGithubResourceClient _resourceClient;
    private readonly IGithubUserClient _userClient;

    public ScopedProjectsUpdaterService(ILogger<ScopedProjectsUpdaterService> logger, 
        IOptionsMonitor<UserConfiguration> config,
        MyProjectsContext ctx,
        IGithubResourceClient resourceClient,
        IGithubUserClient userClient)
    {
        _logger = logger;
        _config = config;
        _ctx = ctx;
        _resourceClient = resourceClient;
        _userClient = userClient;
    }
    
    public async Task DoWork(CancellationToken cancellationToken)
    {
        while (cancellationToken.IsCancellationRequested == false)
        {
            _executionCount++;
            _logger.LogInformation("Scoped Project Updater Service is working. Cycles count: {Count}"
                , _executionCount);
       
            // Загружаем инфу о проектах
            var fetchedProjects = await FetchProjects();
            
            try
            {
                foreach (var project in fetchedProjects)
                {
                    var isExists = _ctx.Projects.Any(x => x.ID == project.ID);
                    
                    if (isExists == false) _ctx.Projects.Add(project);
                    else _ctx.Projects.Attach(project);

                    await _ctx.SaveChangesAsync(cancellationToken);
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to update user repos, error: {}", e.Message);
            }
            
            _logger.LogInformation("Setted periodicity (seconds) : {TotalSeconds}", _config.CurrentValue.UpdatePeriodicity.TotalSeconds);
            
            var peridodicity = (int)_config.CurrentValue.UpdatePeriodicity.TotalMilliseconds;

            await Task.Delay(peridodicity, cancellationToken);
        }
    }
    
    
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

                var techs = new List<Technology>();
                foreach (var techName in repoInfo.Technologies)
                {
                    var existingTech = _ctx.Technologies.FirstOrDefault(x => x.Name == techName);
                    techs.Add(existingTech is not null
                        ? _ctx.Technologies.Attach(existingTech).Entity
                        : _ctx.Technologies.Add(new Technology { Name = techName }).Entity);
                }

                await _ctx.SaveChangesAsync();
                
                var newProject = new Project
                {
                    ID             = repo.Id,
                    CreatedAt      = repo.CreatedAt,
                    UpdatedAt      = repo.UpdatedAt,
                    RepositoryName = repo.Name,
                    MainTitle      = repoInfo.MainTitle,
                    SubTitle       = repoInfo.SubTitle,
                    Technologies   = techs,
                };
                
                projectsList.Add(newProject);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Unable to update user repos, error: {Error}, with repo: {RepoName}", 
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