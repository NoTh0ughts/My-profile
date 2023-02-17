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
    

    /// <summary> The GetRepositoryInfo function retrieves the README.md file from a repository.</summary>
    ///
    /// <param name="repositoryName"> The name of the repository to be created.</param>
    ///
    /// <returns> A string containing the contents of a repository's readme.md file.</returns>
    public async Task<string?> GetRepositoryInfo(string repositoryName)
    {
        try
        {
            return await _httpClient.GetStringAsync
                ($"{_configuration.Value.GithubUsername}/{repositoryName}/master/README.md");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return await new Task<string?>(() => null);
        }
    }
}