using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace FilmRealm.BLL.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}