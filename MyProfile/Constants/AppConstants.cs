
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace MyProfile.Constants;

public class AppConstants
{
    public class Host
    {
        public static readonly Action<JsonOptions> ConfigureJsonOptions = 
            x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    }
    
    public class Client
    {
        
        
        public const string GithubUsername = "NoTh0ughts";
        public const string GithubUserClient = "https://api.github.com/users/";
        public const string GithubResourceClient = "https://raw.githubusercontent.com/";
    }
    
    public class ExitCode
    {
        public const int UNKNOWN_ERROR = 1;
    }
}