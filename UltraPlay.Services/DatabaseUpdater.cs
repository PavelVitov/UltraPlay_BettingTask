using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using UltraPlay.Data;
using UltraPlay.Data.DTOs;
using UltraPlay.Data.Interfaces;
using UltraPlay.Data.Models;
using UltraPlay.Data.Utils;
using UltraPlay.Services.Interfaces;

namespace UltraPlay.Services;

public class DatabaseUpdater : IDatabaseUpdater
{
    private AppDbContext _ctx;
    private ILogger<DatabaseUpdater> _logger;

    public DatabaseUpdater(AppDbContext ctx, ILogger<DatabaseUpdater> logger)
    {
        _ctx = ctx;
        _logger = logger;
    }

    public async Task UpdateDatabase(SportDTO[] sportDTOs, CancellationToken stoppingToken)
    {
        var events = new List<Event>();
        var matches = new List<Match>();
        var bets = new List<Bet>();
        var odds = new List<Odd>();
        var sports = sportDTOs.Select(sport => sport.ToModel(events, matches, bets, odds)).ToList();

        await DeleteInvalidEntries(_ctx.Events, events);
        await DeleteInvalidEntries(_ctx.Matches, matches, _ctx.MatchInformers);
        await DeleteInvalidEntries(_ctx.Bets, bets, _ctx.BetInformers);
        await DeleteInvalidEntries(_ctx.Odds, odds, _ctx.OddInformers);
        await ApplyUpdates(matches, odds);

        var sportIds = await _ctx.Sports.Select(e => e.ID).ToListAsync(stoppingToken);
        var sportsToAdd = sports.Where(e => !sportIds.Contains(e.ID)).ToList();

        if (sportsToAdd.Count > 0)
        {
            _ctx.Sports.AddRange(sportsToAdd);

        }
        else
        {
            var eventIds = await _ctx.Events.Select(e => e.ID).ToListAsync(stoppingToken);
            var eventsToAdd = events.Where(e => !eventIds.Contains(e.ID)).ToList();
            _ctx.Events.AddRange(eventsToAdd);
        }
        await _ctx.SaveChangesAsync();
        _logger.LogInformation("Updated database.");

    }

    private async Task ApplyUpdates(List<Match> matches, List<Odd> odds)
    {
        foreach (var match in matches)
        {
            var fetchedMatch = await _ctx.Matches.FirstOrDefaultAsync(m => m.ID == match.ID && m.DeletedAt == null);
            if (fetchedMatch != null)
            {
                bool needsUpdate = false;
                if (fetchedMatch.MatchType != match.MatchType)
                {
                    fetchedMatch.MatchType = match.MatchType;
                    needsUpdate = true;
                }
                if (fetchedMatch.StartDate != match.StartDate)
                {
                    fetchedMatch.StartDate = match.StartDate;
                    needsUpdate = true;
                }
                if(needsUpdate)
                {
                    _ctx.MatchInformers.Add(new EntityUpdateInformer<Match>()
                    {
                        Entity = fetchedMatch,
                        EntityId = fetchedMatch.ID,
                        UpdateType = UpdateType.UpdateData,
                    });
                }
            }
        }

        foreach (var odd in odds)
        {
            var fetchedOdd = await _ctx.Odds.FirstOrDefaultAsync(o => o.ID == odd.ID && o.DeletedAt == null);
            if (fetchedOdd != null && fetchedOdd.Value != odd.Value)
            {
                fetchedOdd.Value = odd.Value;
                _ctx.OddInformers.Add(new EntityUpdateInformer<Odd>()
                {
                    Entity = fetchedOdd,
                    EntityId = fetchedOdd.ID,
                    UpdateType = UpdateType.UpdateData,
                });
            }
        }

    }

    private async Task DeleteInvalidEntries<T>(DbSet<T> set, IEnumerable<T> entities, DbSet<EntityUpdateInformer<T>>? informer = null) where T: class, ISoftDeletable
    {
        HashSet<int> ids = entities.Select(e => e.ID).ToHashSet();
        var entitiesToRemove =  await set.Where(e => !ids.Contains(e.ID) && e.DeletedAt == null).ToListAsync();
        foreach (var entityToRemove in entitiesToRemove)
        {
            entityToRemove.DeletedAt = DateTime.UtcNow;
            if(informer != null)
            {
                var log = new EntityUpdateInformer<T>()
                {
                    Entity = entityToRemove,
                    EntityId = entityToRemove.ID,
                    UpdateType = UpdateType.RemoveData,
                };
                informer.Add(log);
            }
        }
    }
}
