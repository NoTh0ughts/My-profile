using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using MyProfile;
using MyProfile.Services.Client.Github;
using Newtonsoft.Json.Linq;

namespace Tests;

public class GithubClient_Tests
{
    [Fact]
    public async Task NotNullNotEmpty_Test()
    {
        var httpClient = new HttpClient {
            BaseAddress = new Uri("https://raw.githubusercontent.com/"),
            DefaultRequestHeaders = 
            { 
                UserAgent =
                {
                    new ProductInfoHeaderValue("NoThoughtsProfile", "1.0")
                }
                
            }
        };

        var userConfigurationFile = "C:\\Users\\Hello\\RiderProjects\\MyProfile\\MyProfile\\UserConfiguration.Development.json";
        var jsonRoot = JObject.Parse(await File.ReadAllTextAsync(userConfigurationFile));
        var userConfigJson = jsonRoot.GetValue("UserConfiguration") as JObject;
        var username = userConfigJson?.GetValue("GitHubUsername").ToString();

        IGithubResourceClient gitHubClient = new GithubResourceClient(httpClient,
            Options.Create(new UserConfiguration() { GithubUsername = username }));

        var repositoryInfo = await gitHubClient.GetRepositoryInfo("MessengerCS");
        Assert.NotNull(repositoryInfo);
    }

    [Fact]
    public async Task IncorrectUsername()
    {
        var username = "dertfyguhjiok";
        var httpClient = new HttpClient {
            BaseAddress = new Uri("https://raw.githubusercontent.com/"),
            DefaultRequestHeaders = 
            { 
                UserAgent =
                {
                    new ProductInfoHeaderValue("NoThoughtsProfile", "1.0")
                }
                
            }
        };
        
        IGithubResourceClient gitHubClient = new GithubResourceClient(httpClient,
            Options.Create(new UserConfiguration() { GithubUsername = username }));

        var repositoryInfo = await Assert.ThrowsAsync<HttpRequestException>(
            () => gitHubClient.GetRepositoryInfo("MessengerCS"));
    }
}