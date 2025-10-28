using Domain.Models;

namespace Services.Interfaces;

public interface IGitHubService
{
    Task<GitHubUser?> GetUserAsync(string username, CancellationToken ct = default);
}