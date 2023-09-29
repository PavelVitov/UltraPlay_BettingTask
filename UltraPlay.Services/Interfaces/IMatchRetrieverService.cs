using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UltraPlay.Data.ViewModels;

namespace UltraPlay.Services.Interfaces
{
     public interface IMatchRetrieverService
    {
        public Task<IList<MatchVM>> GetNext24HoursMatches(CancellationToken stoppingToken);
        public Task<MatchVM> GetMatchById(int matchId, CancellationToken stoppingToken);
    }
}
