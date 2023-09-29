using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System.Xml;

using UltraPlay.Data.DTOs;
using UltraPlay.Services.Interfaces;
using UltraPlay.Services.Options;

namespace UltraPlay.Services;

public class DbSyncService : BackgroundService
{
    private readonly ILogger<DbSyncService> _logger;
    private readonly IServiceScopeFactory _factory;
    private readonly DownloadOptions _downloadOptions;

    public DbSyncService(ILogger<DbSyncService> logger, IServiceScopeFactory factory, IOptions<DownloadOptions> downloadOptions)
    {
        _logger = logger;
        _factory = factory;
        _downloadOptions = downloadOptions.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var counter = 1;
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Running sync service");
            using var scope = _factory.CreateScope();
            var parser = scope.ServiceProvider.GetRequiredService<ISportsXMLParser<SportDTO[]>>();
            var dataProvider = scope.ServiceProvider.GetRequiredService<ISportsXMLProvider>();
            var databaseUpdater = scope.ServiceProvider.GetRequiredService<IDatabaseUpdater>();
            var data = await dataProvider.GetData(stoppingToken);
            var dtos = parser.Parse(data);
            await databaseUpdater.UpdateDatabase(dtos, stoppingToken);
            _logger.LogInformation("Sync service done");
            await Task.Delay(_downloadOptions.Intrevals, stoppingToken);
            //await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();
            //IXMLReader xmlReader = asyncScope.ServiceProvider.GetRequiredService<IXMLReader>();
            //ISportsClient sportsClient = asyncScope.ServiceProvider.GetRequiredService<ISportsClient>();
            //IDatabaseEngine databaseEngine = asyncScope.ServiceProvider.GetRequiredService<IDatabaseEngine>();

            //var xmlData = await sportsClient.GetXmlSportsDataAsync(cToken);
            //var sportDto = xmlReader.ParseSportsDataXML(xmlData);

            //await databaseEngine.UpdateDatabaseAsync(sportDto, cToken);

            //_logger.LogInformation($"Worker running at: {DateTime.UtcNow}, for {counter} time");
            //counter++;

            //await Task.Delay(Constans.DownloadIntervalMiliseconds, cToken);
        }
    }
}
