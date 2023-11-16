using FilmRealm.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace FilmRealm.WebApi.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseFilmRealmContext(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
        using var context = scope?.ServiceProvider.GetRequiredService<FilmRealmContext>();
        context?.Database.Migrate();
    }
}