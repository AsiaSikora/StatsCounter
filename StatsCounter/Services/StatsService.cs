using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StatsCounter.Models;

namespace StatsCounter.Services
{
    public interface IStatsService
    {
        Task<RepositoryStats> GetRepositoryStatsByOwnerAsync(string owner);
    }
    
    public class StatsService : IStatsService
    {
        private readonly IGitHubService _gitHubService;

        public StatsService(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        public async Task<RepositoryStats> GetRepositoryStatsByOwnerAsync(string owner)
        {
            var repos = await _gitHubService.GetRepositoryInfosByOwnerAsync(owner);

            var allNames = repos.Select(x => x.Name).ToList();
            List<char> alphabet = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            Dictionary<char, int> letters = new Dictionary<char, int>();
            
            foreach (var name in allNames)
            {
                foreach (var letter in name)
                {
                    if (alphabet.Contains(Char.ToUpper(letter)))
                    {
                        if (!letters.ContainsKey(Char.ToLower(letter)))
                        {
                            letters.Add(Char.ToLower(letter), 1);
                        }
                        else
                        {
                            letters[Char.ToLower(letter)] += 1;
                        }
                    }
                }
            }

            var stats = new RepositoryStats()
            {
                Owner = owner,
                AvgSize = repos.Average(x => x.Size),
                AvgForks = repos.Average(x => x.ForksCount),
                AvgStargazers = repos.Average(x => x.StargazersCount),
                AvgWatchers = repos.Average(x => x.WatchersCount),
                Letters = letters
            };
            
            return stats;
        }
    }
}