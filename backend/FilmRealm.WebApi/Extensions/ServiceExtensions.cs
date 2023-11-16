using FilmRealm.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace FilmRealm.WebApi.Extensions;

public static class ServiceExtensions
{
    public static void AddFilmRealmContext(this IServiceCollection services, IConfiguration configuration)
    {
        var migrationAssembly = typeof(FilmRealmContext).Assembly.GetName().Name;
        services.AddDbContext<FilmRealmContext>(options =>
            options.UseSqlServer(configuration["ConnectionStrings:FilmRealmDBConnection"],
                opt => opt.MigrationsAssembly(migrationAssembly)));
    }
}