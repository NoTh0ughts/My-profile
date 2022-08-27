﻿using Microsoft.EntityFrameworkCore;
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

    public async Task DoWork(CancellationToken cancellationToken)
    {
        while (cancellationToken.IsCancellationRequested == false)
        {
            _executionCount++;
            
            _logger.LogInformation("Scoped Project Updater Service is working. Count: {Count}", _executionCount);

            
            // Work here
            try
            {
                var projects = await FetchProjects();

                foreach (var project in projects)
                {
                    var isExists = _dbContext.Projects.Any(x => x.ID == project.ID);
                    if (isExists)
                    {
                        // TODO : Проверить эту штуку
                        var existingProject =
                            await _dbContext.Projects
                                .FirstOrDefaultAsync(x => x.ID == project.ID, cancellationToken);

                        existingProject.UpdatedAt = project.UpdatedAt;
                        existingProject.MainTitle = project.MainTitle;
                        existingProject.SubTitle = project.SubTitle;
                        existingProject.Technologies = project.Technologies;
                        existingProject.RepositoryName = project.RepositoryName;
                        
                        await _dbContext.SaveChangesAsync(cancellationToken);
                    }
                    else
                    {
                        await _dbContext.AddAsync(project, cancellationToken);
                        await _dbContext.SaveChangesAsync(cancellationToken);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to update user repos, error: {}", e.Message);
            }

            var peridodicity = (int)_config.CurrentValue.UpdatePeriodicity.TotalMilliseconds;
            await Task.Delay(peridodicity, cancellationToken);
        }
    }

    private async Task<IEnumerable<Project>> FetchProjects()
    {
        var userRepos = await _userClient.GetUserRepositories();
        var projectsList = new List<Project>(userRepos.Count);
        
        for (var i = 0; i < userRepos.Count; i++)
        { 
            try
            {
                var stringRepoInfo = await _resourceClient.GetRepositoryInfo(userRepos[i].Name);
                var repoInfo = ParseFromHtml(stringRepoInfo);
                foreach (var technology in repoInfo.Technologies)
                {
                    _dbContext.Technologies.AddIfNotExists(new Technology
                    {
                        Name = technology
                    }, t => t.Name == technology);
                }
                
                await _dbContext.SaveChangesAsync();
                
                var actualTechs = _dbContext.Technologies
                    .Where(t => repoInfo.Technologies.Contains(t.Name))
                    .ToList();

                var newProject = new Project
                {
                    ID = userRepos[i].Id,
                    CreatedAt = userRepos[i].CreatedAt,
                    UpdatedAt = userRepos[i].UpdatedAt,
                    RepositoryName = userRepos[i].Name,
                    MainTitle = repoInfo.MainTitle,
                    SubTitle = repoInfo.SubTitle,
                    Technologies = new List<Technology>(actualTechs),
                };
            
                projectsList.Add(newProject);
            }
            catch (Exception e)
            {
                _logger.LogError("Unable to update user repos, error: {Error}, with repo: {RepoName}", 
                    e.Message, userRepos[i].Name);
            }
        }

        return projectsList;
    }

    private RepositoryInfoResponse ParseFromHtml(string htmlString)
    {
        HtmlDocument html = new HtmlDocument();
        html.LoadHtml(htmlString);

        var repoInfo = new RepositoryInfoResponse()
        {
            MainTitle = html.GetElementSingleValue("MainTitle"),
            SubTitle = html.GetElementSingleValue("SubTitle"),
            Technologies = html.GetElementListValue("TechStack", '*').ToArray()
        };

        return repoInfo;
    }
}