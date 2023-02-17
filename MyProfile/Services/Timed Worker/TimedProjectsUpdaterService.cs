namespace MyProfile.Services.Timed_Worker;

public class TimedProjectsUpdaterService : BackgroundService
{
    private IServiceProvider Services { get; }
    
    private readonly ILogger<TimedProjectsUpdaterService> _logger;

    public TimedProjectsUpdaterService(ILogger<TimedProjectsUpdaterService> logger, IServiceProvider services)
    {
        _logger = logger;
        Services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Projects Updater Service is running");

        await DoWork(stoppingToken);
    }
    
    private async Task DoWork(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Projects Updater Service is running");

        using var scope = Services.CreateScope();
        
        var scopedService = scope.ServiceProvider.GetRequiredService<IScopedProjectUpdaterService>();
        await scopedService.DoWork(stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Timed Projects Updater Service is stopping");
        
        await base.StopAsync(cancellationToken);
    }
}