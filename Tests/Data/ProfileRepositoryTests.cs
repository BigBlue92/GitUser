using Data;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.Data;

public class ProfileRepositoryTests
{
    private static ApplicationDbContext CreateInMemoryContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;
        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task AddOrUpdate_Then_GetByLogin_ReturnsEntity()
    {
        using var db = CreateInMemoryContext(nameof(AddOrUpdate_Then_GetByLogin_ReturnsEntity));
        var repo = new ProfileRepository(db);

        var entity = new GitHubProfileEntity
        {
            Login = "octocat",
            Name = "Octo",
            AvatarUrl = "https://avatar",
            HtmlUrl = "https://github.com/octocat",
            Bio = "bio",
            PublicRepos = 10,
            Followers = 5,
            Following = 2
        };

        await repo.AddOrUpdateAsync(entity);
        await repo.SaveChangesAsync();

        var got = await repo.GetByLoginAsync("octocat");
        Assert.NotNull(got);
        Assert.Equal(entity.Login, got!.Login);
    }
}