using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using Services.Services;

namespace Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGitUserServices(this IServiceCollection services)
    {
        // typed HttpClient for GitHub public API
        services.AddHttpClient<IGitHubService, GitHubService>(client =>
        {
            client.BaseAddress = new Uri("https://api.github.com/");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("GitUserApp/1.0");
        });

        // register other service implementations here

        return services;
    }
}
