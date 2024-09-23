using System.Text.Json;
using Microsoft.Extensions.Options;

namespace MyProfile.Services.Client.LeetCode;

public class LeetCodeClient : ILeetCodeClient
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<UserConfiguration> _configuration;
    private readonly ILogger<LeetCodeClient> _logger;

    public LeetCodeClient(HttpClient httpClient, 
        IOptions<UserConfiguration> configuration, 
        ILogger<LeetCodeClient> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<LeetCodeUserInfoResponse?> GetUserInfo(CancellationToken token = default)
    {
        var uri = new UriBuilder
        {
            Host = "leetcode-stats-api.herokuapp.com",
            Path = _configuration.Value.LeetCodeUsername,
            Scheme = "https"
        };

        try
        {
            var response = await _httpClient.GetStringAsync(uri.Uri, cancellationToken: token);

            var result = JsonSerializer.Deserialize<LeetCodeUserInfoResponse>(response);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError("LeetCodeClient GetUserInfo exception: {Message}", e.Message);
        }

        return null;
    }
}