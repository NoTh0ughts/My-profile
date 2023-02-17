using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyProfile;
using MyProfile.Constants;
using MyProfile.Services;
using MyProfile.Services.Client.Github;
using MyProfile.Services.Timed_Worker;

var builder = WebApplication.CreateBuilder(args);

// Добавление конф файлов
{
    builder.Configuration.AddJsonFile($"UserConfiguration.{builder.Environment.EnvironmentName}.json", false, true);
}


// Добавление сервисов в контейнер DI
{
    builder.Services.AddSwaggerGen(c =>
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
                Url = new Uri("https://github.com/NoTh0ughts/My-profile")
            }
        });
    });
    /*builder.Services.AddCors();*/
    
    builder.Services.AddLogging();
    
    builder.Services.AddControllersWithViews()
        .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

    builder.Services.AddDbContext<MyProjectsContext>(optionsBuilder =>
    {
        try
        {
            var root = Directory.GetCurrentDirectory();
            var dotenv = Path.Combine(root, ".env");
            EnvConfiguration.Load(dotenv);
        
            var connectionString = EnvConfiguration.MySqlUrl;
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), 
                x => x.MigrationsAssembly("Migrations"));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    });

    
    var productInfoHeaderValue = new ProductInfoHeaderValue("NoThoughtsProfile", "1.0");
    
    builder.Services.AddSingleton<IGithubUserClient, GithubUserClient>();
    builder.Services.AddHttpClient<IGithubUserClient, GithubUserClient>(client =>
    {
        client.BaseAddress = new Uri(AppConstants.Client.GithubUserClient);
        client.DefaultRequestHeaders.UserAgent.Add(productInfoHeaderValue);
    });

    builder.Services.AddSingleton<IGithubResourceClient, GithubResourceClient>();
    builder.Services.AddHttpClient<IGithubResourceClient, GithubResourceClient>(client =>
    {
        client.BaseAddress = new Uri(AppConstants.Client.GithubResourceClient);
        client.DefaultRequestHeaders.UserAgent.Add(productInfoHeaderValue);
    });

    builder.Services.AddOptions<UserConfiguration>()
        .Bind(builder.Configuration.GetSection(UserConfiguration.SectionName))
        .ValidateDataAnnotations();
    
    // Добавление Background сервисов
    {
        builder.Services.AddHostedService<TimedProjectsUpdaterService>();
        builder.Services.AddScoped<IScopedProjectUpdaterService, ScopedProjectsUpdaterService>();
    }
}


var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyProfile v1"));
    
    /*app.UseCors(policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });*/
    
    app.UseStaticFiles();
    app.UseRouting();
    
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");

    app.UseEndpoints(endpoints => endpoints.MapControllers());
    
    app.MapFallbackToFile("index.html");;

    app.Run();
}
