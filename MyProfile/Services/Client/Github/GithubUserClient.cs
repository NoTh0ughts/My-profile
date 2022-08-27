﻿using Microsoft.Extensions.Options;

namespace MyProfile.Services.Client.Github;

public class GithubUserClient : IGithubUserClient
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<UserConfiguration> _configuration;

    public GithubUserClient(HttpClient httpClient, IOptions<UserConfiguration> configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }
    
    public async Task<List<RepositoryInfo>?> GetUserRepositories()
    {
        return await _httpClient.GetFromJsonAsync<List<RepositoryInfo>>
            ($"{_configuration.Value.GithubUsername}/repos?type=owner");
    }
}