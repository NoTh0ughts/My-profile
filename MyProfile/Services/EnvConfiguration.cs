﻿namespace MyProfile.Services;

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
        Console.WriteLine($"Connection string : {result}");
        return _mySQL = result;
    }
   
}