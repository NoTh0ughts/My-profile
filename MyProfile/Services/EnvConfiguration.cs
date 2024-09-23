namespace MyProfile.Services;

public static class EnvConfiguration
{
    public static string MySqlUrl => _mySql ??= ConfigureMysqlFromEnvironment();
    public static string RedisConf => _redisConf ??= ConfigureRedisConf();

    private static string ConfigureRedisConf()
    {
        var address = Environment.GetEnvironmentVariable("SERVER");
        var port    = Environment.GetEnvironmentVariable("PORT");
        var password= Environment.GetEnvironmentVariable("REDIS_PASSWORD");
        var result = $"{address}:{port},password={password}";
        return result;
    }

    private static string? _mySql;
    private static string? _redisConf;

    private static string ConfigureMysqlFromEnvironment()
    {
        var address      = Environment.GetEnvironmentVariable("MYSQL_SERVER");
        var port         = Environment.GetEnvironmentVariable("MYSQL_PORT");
        var userId       = Environment.GetEnvironmentVariable("MYSQL_USER");
        var password     = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
        var databaseName = Environment.GetEnvironmentVariable("MYSQL_DATABASE");

        var result = $@"Server={address};Port={port};Database={databaseName};Uid={userId};Pwd={password};";
        return _mySql = result;
    }
}