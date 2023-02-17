namespace MyProfile.Services;

public static class EnvConfiguration
{
    public static string MySqlUrl => _mySQL ??= ConfigureURLMYSQLFromEnviroment();
    private static string? _mySQL;
    private static string ConfigureURLMYSQLFromEnviroment()
    {
        var address = Environment.GetEnvironmentVariable("MYSQL_SERVER");
        var port = Environment.GetEnvironmentVariable("MYSQL_PORT");
        var userId = "root";
        var password = Environment.GetEnvironmentVariable("MYSQL_ROOT_PASSWORD");
        var databaseName = Environment.GetEnvironmentVariable("MYSQL_DATABASE");

        var result = $@"Server={address};Port={port};Database={databaseName};Uid={userId};Pwd={password};";

        return _mySQL = result;
    }
    
    /// <summary>
    /// Загружает данные из файла .env, добавляет все данные в переменные окружения
    /// Данные должны быть в формате key = value
    /// </summary>
    /// <param name="filename"></param>
    public static void Load(string filename)
    {
        if(!File.Exists(filename))
        {
            Console.WriteLine("File does not exists");
            return;
        }

        string[] lines = File.ReadAllLines(filename);
            
        foreach (var line in lines)
        {
            // Отделяем ключ и значение
            var parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries);
            // Добавляем полученные значения в переменные окружения
            if (parts.Length == 2) Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }

        // Строим конфиг
        new ConfigurationBuilder().AddEnvironmentVariables().Build();
    }
}