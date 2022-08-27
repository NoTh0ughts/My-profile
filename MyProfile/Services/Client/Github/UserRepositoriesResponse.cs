using System.Text.Json.Serialization;

namespace MyProfile.Services.Client.Github;

/// <summary>
/// Модель данных ответов о репозитории
/// </summary>
public struct RepositoryInfo
{   
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
}