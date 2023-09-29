using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using UltraPlay.Data;
using UltraPlay.Data.Utils;
using UltraPlay.Data.ViewModels;
using UltraPlay.Services.Interfaces;

namespace UltraPlay.Services
{
    // Service for retrieving match-related data.
    public class MatchRetrieverService : IMatchRetrieverService
    {
        // Define a set of preview bets.
        private static readonly HashSet<string> previewBets = new() { "Match Winner", "Map Advantage", "Total Maps Played" };

        // Logger for logging information.
        private readonly ILogger<MatchRetrieverService> _logger;
        // Database context for accessing data.
        private readonly AppDbContext _ctx;

        // Constructor to initialize the service with a logger and database context.
        public MatchRetrieverService(ILogger<MatchRetrieverService> logger, AppDbContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        // Method to retrieve a match by its ID.
        public async Task<MatchVM> GetMatchById(int matchId, CancellationToken stoppingToken)
        {
            // Log the information about fetching a specific match.
            _logger.LogInformation("Fetching {matchId}", matchId);
            // Query the database to retrieve the match details.
            var model = await _ctx.Matches
                .Where(x => x.ID == matchId && x.DeletedAt == null)
                .Select(x => new MatchVM
                {
                    ID = x.ID,
                    Name = x.Name,
                    StartDate = x.StartDate,
                    ActiveBets = x.Bets.Where(b => b.DeletedAt == null).Select(b => new BetVM
                    {
                        ID = b.ID,
                        Name = b.Name,
                        Odds = b.Odds.Where(o => o.DeletedAt == null).Select(o => new OddVM
                        {
                            ID = o.ID,
                            Name = o.Name,
                            SpecialBetValue = o.SpecialBetValue,
                            Value = o.Value
                        }).ToList(),
                    }).ToList(),
                    InactiveBets = x.Bets.Where(b => b.DeletedAt != null).Select(b => new BetVM
                    {
                        ID = b.ID,
                        Name = b.Name,
                        Odds = b.Odds.Select(o => new OddVM
                        {
                            ID = o.ID,
                            Name = o.Name,
                            SpecialBetValue = o.SpecialBetValue,
                            Value = o.Value
                        }).ToList(),
                    }).ToList()
                })
                .FirstOrDefaultAsync(stoppingToken);
            // Return the retrieved match data.
            return model;
        }

        // Method to retrieve a list of matches for the next 24 hours.
        public async Task<IList<MatchVM>> GetNext24HoursMatches(CancellationToken stoppingToken)
        {
            // Create a local copy of preview bets.
            var _previewBets = previewBets;

            // Log the information about fetching matches for the next 24 hours.
            _logger.LogInformation("Fetching next 24 hour matches");
            // Query the database to retrieve matches that match the specified criteria.
            var models = await _ctx.Matches
               .Where(m => m.StartDate >= DateTime.UtcNow && m.StartDate <= DateTime.UtcNow.AddHours(24) && m.DeletedAt == null)
               .Include(m => m.Bets)
               .Select(m => m.ToVM(_previewBets))
               .ToListAsync(stoppingToken);

            // Extract a list of active bets from the retrieved matches.
            var bets = models.SelectMany(m => m.ActiveBets).ToList();

            // Process and populate odds data for the active bets.
            foreach (var bet in bets)
            {
                var odds = _ctx.Odds.Where(o => o.BetId == bet.ID && o.DeletedAt == null).Select(o => new OddVM
                {
                    ID = o.ID,
                    Name = o.Name,
                    SpecialBetValue = o.SpecialBetValue,
                    Value = o.Value,
                }).ToList();

                if (odds.All(o => o.SpecialBetValue != null))
                {
                    var groupOdd = odds.GroupBy(o => o.SpecialBetValue)
                         .Select(o => new OddVM
                         {
                             SpecialBetValue = o.Key,
                             ID = o.Select(x => x.ID).FirstOrDefault(),
                             Value = o.Select(x => x.Value).FirstOrDefault(),
                             Name = o.Select(x => x.Name).FirstOrDefault(),
                         })
                         .FirstOrDefault();

                    bet.Odds.Add(groupOdd);
                }
                else
                {
                    bet.Odds.AddRange(odds);
                }
            }

            // Return the list of matches with populated odds data.
            return models;
        }
    }
}
