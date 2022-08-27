using Microsoft.Extensions.Options;

namespace MyProfile.Services.Client.Github;

public class GithubResourceClient : IGithubResourceClient
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<UserConfiguration> _configuration;
    
    public GithubResourceClient(HttpClient httpClient, IOptions<UserConfiguration> configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }
    
    public async Task<string?> GetRepositoryInfo(string repositoryName)
    {
        return await _httpClient.GetStringAsync
            ($"{_configuration.Value.GithubUsername}/{repositoryName}/master/README.md");
    }
}