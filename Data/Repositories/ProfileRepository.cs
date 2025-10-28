using Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProfileRepository : IProfileRepository
{
    private readonly ApplicationDbContext _db;
    public ProfileRepository(ApplicationDbContext db) => _db = db;

    public async Task<GitHubProfileEntity?> GetByLoginAsync(string login, CancellationToken ct = default)
    {
        return await _db.GitHubProfiles.AsNoTracking()
            .FirstOrDefaultAsync(p => p.Login == login, ct);
    }

    public async Task AddOrUpdateAsync(GitHubProfileEntity entity, CancellationToken ct = default)
    {
        var existing = await _db.GitHubProfiles.FirstOrDefaultAsync(p => p.Login == entity.Login, ct);
        if (existing is null)
        {
            await _db.GitHubProfiles.AddAsync(entity, ct);
        }
        else
        {
            existing.Name = entity.Name;
            existing.AvatarUrl = entity.AvatarUrl;
            existing.HtmlUrl = entity.HtmlUrl;
            existing.Bio = entity.Bio;
            existing.PublicRepos = entity.PublicRepos;
            existing.Followers = entity.Followers;
            existing.Following = entity.Following;
            existing.QueriedAtUtc = DateTime.UtcNow;
            _db.GitHubProfiles.Update(existing);
        }
    }

    public Task SaveChangesAsync(CancellationToken ct = default) => _db.SaveChangesAsync(ct);
}
