namespace MyProfile.Services.Client.Github;

public interface IGithubResourceClient
{
    public Task<string?> GetRepositoryInfo(string repositoryName);
}