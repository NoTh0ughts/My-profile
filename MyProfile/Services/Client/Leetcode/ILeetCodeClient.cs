namespace MyProfile.Services.Client.LeetCode;

public interface ILeetCodeClient
{
    public Task<LeetCodeUserInfoResponse?> GetUserInfo(CancellationToken token);
}