namespace MyProfile.Services.Client.Github;

public interface IGithubUserClient
{
    public Task<List<RepositoryInfo>?> GetUserRepositories();
}
