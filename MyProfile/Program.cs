using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using MyProfile.Constants;
using MyProfile.Services;
using MyProfile.Services.Cache;
using MyProfile.Services.HostExt;

var builder = WebApplication.CreateBuilder(args);

// Добавление конф файлов
builder.AddHostOptions();

// Добавление сервисов в контейнер DI
{
     // Регистрируем CacheService в качестве Singleton или Scoped
    builder.Services.AddSwagger();
    builder.Services.AddMySqlDb();
    builder.Services.AddLogging();
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.AddControllersWithViews().AddJsonOptions(AppConstants.Host.ConfigureJsonOptions);
    builder.Services.AddExternalHttpClients();
    builder.Services.AddSpaStaticFiles(configuration => {
        configuration.RootPath = "wwwroot";
    });
    
    builder.Services.AddGithubUserInfoUpdates();
    builder.Services.AddScoped<ICacheService, CacheService>();
    builder.Services.AddStackExchangeRedisCache(ro =>
    {
        ro.Configuration = EnvConfiguration.RedisConf;
    });
}

var app = builder.Build();
{
    app.UseSwaggerIfDev();
    app.UseStaticFiles(new StaticFileOptions());
    app.UseRouting();
    app.UseMigrations();    

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
    
    app.UseSpa(spa => {
        spa.Options.SourcePath = "ClientApp";

        if (app.Environment.IsDevelopment()) {
            spa.UseReactDevelopmentServer(npmScript: "start");
        }
    });

    app.UseEndpoints(endpoints => endpoints.MapControllers());
    app.MapFallbackToFile("index.html");;

    app.Run();
}
