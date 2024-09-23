using System.Net.Http.Headers;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyProfile.Constants;
using MyProfile.Services.Client.Github;
using MyProfile.Services.Client.LeetCode;
using MyProfile.Services.Timed_Worker;

namespace MyProfile.Services.HostExt;

internal static class HostExtensions
{
    internal static WebApplicationBuilder AddHostOptions(this WebApplicationBuilder wb)
    {
        wb.Configuration.AddJsonFile($"UserConfiguration.{wb.Environment.EnvironmentName}.json", false, true);
        wb.Services.AddOptions<UserConfiguration>()
            .Bind(wb.Configuration.GetSection(UserConfiguration.SectionName))
            .ValidateDataAnnotations();
        
        return wb;
    }
    
    internal static IServiceCollection AddGithubUserInfoUpdates(this IServiceCollection services)
    {
        services.AddHostedService<TimedProjectsUpdaterService>();
        services.AddScoped<IScopedProjectUpdaterService, ScopedProjectsUpdaterService>();
        
        return services;
    }
    
    internal static IServiceCollection AddMySqlDb(this IServiceCollection services)
    {
        services.AddDbContext<MyProjectsContext>(optionsBuilder =>
            {  
                optionsBuilder.UseMySql(EnvConfiguration.MySqlUrl, 
                    ServerVersion.AutoDetect(EnvConfiguration.MySqlUrl),
                    x => x.MigrationsAssembly("Migrations"));
            });

        return services;
    }
    
    internal static IServiceCollection AddExternalHttpClients(this IServiceCollection services)
    {
        var productInfoHeaderValue = new ProductInfoHeaderValue("NoThoughtsProfile", "1.0");
    
        services.AddSingleton<IGithubUserClient, GithubUserClient>();
        services.AddHttpClient<IGithubUserClient, GithubUserClient>(client =>
        {
            client.BaseAddress = new Uri(AppConstants.Client.GithubUserClient);
            client.DefaultRequestHeaders.UserAgent.Add(productInfoHeaderValue);
        });
    
        services.AddSingleton<IGithubResourceClient, GithubResourceClient>();
        services.AddHttpClient<IGithubResourceClient, GithubResourceClient>(client =>
        {
            client.BaseAddress = new Uri(AppConstants.Client.GithubResourceClient);
            client.DefaultRequestHeaders.UserAgent.Add(productInfoHeaderValue);
        });
        
        services.AddSingleton<ILeetCodeClient, LeetCodeClient>();
        services.AddHttpClient<ILeetCodeClient, LeetCodeClient>(client =>
        {
            client.DefaultRequestHeaders.UserAgent.Add(productInfoHeaderValue);
        });
        
        return services;
    }
    
    internal static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "<My profile>",
                Version = "v1",
                Description = "My profile",
                Contact = new OpenApiContact
                {
                    Name = "Repo:",
                    Email = "",
                    Url = new Uri("https://github.com/NoTh0ughts/My-profile"),
                }
            });
        });
        
        return services;
    }

    internal static WebApplication UseSwaggerIfDev(this WebApplication app)
    {
        if (app.Environment.IsDevelopment() == false) return app;
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyProfile v1"));

        return app;
    }
    
    internal static WebApplication UseMigrations(this WebApplication app)
    {
        var context = app.Services.GetRequiredService<MyProjectsContext>();
        if (context.Database.GetPendingMigrations().Any()) context.Database.Migrate();
        
        return app;
    }
    
    
}