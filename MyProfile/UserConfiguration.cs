using System.ComponentModel.DataAnnotations;

namespace MyProfile;

public class UserConfiguration
{
    public const string SectionName = "UserConfiguration";
    
    public TimeSpan UpdatePeriodicity { get; set; }

    [RegularExpression(@"^[a-zA-Z\d](?:[a-zA-Z\d]|-(?=[a-zA-Z\d])){0,38}$")]
    public string GithubUsername { get; set; } = "";
}