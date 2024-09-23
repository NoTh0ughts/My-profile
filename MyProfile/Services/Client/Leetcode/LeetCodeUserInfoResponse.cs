namespace MyProfile.Services.Client.LeetCode;

using System.Text.Json.Serialization;

public struct LeetCodeUserInfoResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("totalSolved")]
    public int TotalSolved { get; set; }

    [JsonPropertyName("totalQuestions")]
    public int TotalQuestions { get; set; }

    [JsonPropertyName("easySolved")]
    public int EasySolved { get; set; }

    [JsonPropertyName("totalEasy")]
    public int TotalEasy { get; set; }

    [JsonPropertyName("mediumSolved")]
    public int MediumSolved { get; set; }

    [JsonPropertyName("totalMedium")]
    public int TotalMedium { get; set; }

    [JsonPropertyName("hardSolved")]
    public int HardSolved { get; set; }

    [JsonPropertyName("totalHard")]
    public int TotalHard { get; set; }

    [JsonPropertyName("acceptanceRate")]
    public double AcceptanceRate { get; set; }

    [JsonPropertyName("ranking")]
    public int Ranking { get; set; }

    [JsonPropertyName("contributionPoints")]
    public int ContributionPoints { get; set; }

    [JsonPropertyName("reputation")]
    public int Reputation { get; set; }
}