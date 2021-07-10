using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(ApplicationDbContext).Assembly.FullName;

            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(x => 
                x.UseSqlServer(connectionString, b => b.MigrationsAssembly(migrationsAssembly)));
        }
    }
}