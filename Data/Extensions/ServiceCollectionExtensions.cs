using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGitUserData(this IServiceCollection services, IConfiguration config, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseInMemoryDatabase("GitUserDev"));
        }
        else
        {
            var conn = config.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Missing DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseSqlServer(conn));
        }

        services.AddScoped<IProfileRepository, ProfileRepository>();
        return services;
    }
}
