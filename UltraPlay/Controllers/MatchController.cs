using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using UltraPlay.Services.Interfaces;

namespace UltraPlay.Controllers
{
    // This controller handles API requests related to matches.
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchRetrieverService _retrieverService;

        // Constructor that injects the MatchRetrieverService dependency.
        public MatchController(IMatchRetrieverService retrieverService)
        {
            _retrieverService = retrieverService;
        }

        // This endpoint retrieves match details by match ID.
        [HttpGet("{matchId}")]
        public async Task<ActionResult> GetMatchByIdAsync(int matchId, CancellationToken stoppingToken)
        {
            // Call the service to retrieve match details.
            var match = await _retrieverService.GetMatchById(matchId, stoppingToken);
            
            // If the match is not found, return a 404 (Not Found) response.
            if (match == null)
            {
                return NotFound(match);
            }
            
            // Return a 200 (OK) response with the retrieved match.
            return Ok(match);
        }

        // This endpoint retrieves matches scheduled for the next 24 hours.
        [HttpGet("24hours")]
        public async Task<ActionResult> GetNext24HoursMatches(CancellationToken stoppingToken)
        {
            // Call the service to retrieve matches scheduled for the next 24 hours.
            var matches = await _retrieverService.GetNext24HoursMatches(stoppingToken);
            
            // Return a 200 (OK) response with the list of retrieved matches.
            return Ok(matches);
        }
    }
}
