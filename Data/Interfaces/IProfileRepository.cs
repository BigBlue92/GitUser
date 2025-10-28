using Data.Entities;

namespace Data.Interfaces;

public interface IProfileRepository
{
    Task<GitHubProfileEntity?> GetByLoginAsync(string login, CancellationToken ct = default);
    Task AddOrUpdateAsync(GitHubProfileEntity entity, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
