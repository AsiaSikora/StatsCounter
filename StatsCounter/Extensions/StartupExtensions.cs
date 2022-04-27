using System;
using Microsoft.Extensions.DependencyInjection;
using StatsCounter.Services;

namespace StatsCounter.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddGitHubService(
            this IServiceCollection services, Uri baseApiUrl)
        {
            services.AddHttpClient<IGitHubService, GitHubService>(httpClient =>
            {
                httpClient.BaseAddress = baseApiUrl;
                httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                httpClient.DefaultRequestHeaders.Add("User-Agent", "StatsCounter");
            });

            return services;
        }
    }
}