namespace MyProfile.Services;

public static class EnvConfiguration
{
    public static string MySqlUrl => _mySQL ??= ConfigureURLMYSQLFromEnviroment();
    private static string? _mySQL;
    private static string ConfigureURLMYSQLFromEnviroment()
    {
        var address = Environment.GetEnvironmentVariable("MYSQL_SERVER");
        var port = Environment.GetEnvironmentVariable("MYSQL_PORT");
        var userId = Environment.GetEnvironmentVariable("MYSQL_USER");
        var password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
        var databaseName = Environment.GetEnvironmentVariable("MYSQL_DATABASE");

        var result = $@"Server={address};Port={port};Database={databaseName};Uid={userId};Pwd={password};";
        Console.WriteLine($"Connection string : {result}");
        return _mySQL = result;
    }
   
}