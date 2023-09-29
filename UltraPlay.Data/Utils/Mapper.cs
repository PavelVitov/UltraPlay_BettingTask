using System.Text.RegularExpressions;
using UltraPlay.Data.DTOs;
using UltraPlay.Data.Models;
using UltraPlay.Data.ViewModels;
using Match = UltraPlay.Data.Models.Match;

namespace UltraPlay.Data.Utils
{
    public static class Mapper
    {

        public static MatchVM ToVM(this Match model, HashSet<string> allowed)
        {
            return new MatchVM
            {
                ID = model.ID,
                Name = model.Name,
                StartDate = model.StartDate,
                ActiveBets = model.Bets.Where(b => b.DeletedAt == null && allowed.Contains(b.Name))
                                       .Select(b => b.ToVM()).ToList(),
                InactiveBets = new List<BetVM>()
            };
        }

        public static BetVM ToVM(this Bet model)
        {
            return new BetVM
            {
                ID = model.ID,
                Name = model.Name,
                Odds = new List<OddVM>()
            };
        }
        public static Sport ToModel(this SportDTO dto,
                                    List<Event>? events = null,
                                    List<Match>? matches = null,
                                    List<Bet>? bets = null,
                                    List<Odd>? odds = null)
        {
            var sport = new Sport()
            {
                Name = dto.Name,
                ID = dto.ID,
                Events = dto.Events.Select(ev => ev.ToModel(dto.ID, events, matches, bets, odds)).ToHashSet(),
            };
            return sport;
        }

        public static Event ToModel(this EventDTO dto,
                                    int sportId,
                                    List<Event>? events = null,
                                    List<Match>? matches = null,
                                    List<Bet>? bets = null,
                                    List<Odd>? odds = null)
        {
            var ev = new Event()
            {
                CategoryID = dto.CategoryID,
                ID = dto.ID,
                IsLive = dto.IsLive,
                Name = dto.Name,
                SportID = sportId,
                Matches = dto.Matches.Select(match => match.ToModel(dto.ID, matches, bets, odds)).ToHashSet(),
            };
            events?.Add(ev);
            return ev;
        }

        public static Match ToModel(this MatchDTO dto,
                                    int eventId,
                                    List<Match>? matches = null,
                                    List<Bet>? bets = null,
                                    List<Odd>? odds = null)
        {
            var match = new Match()
            {
                EventId = eventId,
                ID = dto.ID,
                MatchType = Enum.Parse<MatchType>(dto.MatchType),
                Name = dto.Name,
                StartDate = dto.StartDate,
                Bets = dto.Bets.Select(bet => bet.ToModel(dto.ID, bets, odds)).ToHashSet(),
            };
            matches?.Add(match);
            return match;
        }

        public static Bet ToModel(this BetDTO dto,
                                  int matchId,
                                  List<Bet>? bets = null,
                                  List<Odd>? odds = null)
        {
            var bet = new Bet()
            {
                ID = dto.ID,
                IsLive = dto.IsLive,
                MatchId = matchId,
                Name = dto.Name,
                Odds = dto.Odds.Select(odd => odd.ToModel(dto.ID, odds)).ToHashSet(),
            };
            bets?.Add(bet);
            return bet;
        }

        public static Odd ToModel(this OddDTO dto,
                                 int betId,
                                 List<Odd>? odds = null)
        {
            var odd = new Odd()
            {
                ID = dto.ID,
                Name = dto.Name,
                SpecialBetValue = dto.SpecialBetValue == "0" ? null : dto.SpecialBetValue,
                Value = dto.Value,
                BetId = betId,
            };
            odds?.Add(odd);
            return odd;
        }

    }
}


