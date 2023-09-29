using UltraPlay.Data.DTOs;

namespace UltraPlay.Services.Interfaces;

public interface IDatabaseUpdater
{
    public Task UpdateDatabase(SportDTO[] sportDTOs, CancellationToken stoppingToken);
}
