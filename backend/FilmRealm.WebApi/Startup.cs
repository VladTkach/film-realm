using FilmRealm.BLL.Extensions;
using FilmRealm.WebApi.Extensions;
using FilmRealm.WebApi.Middlewares;

namespace FilmRealm.WebApi;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddFilmRealmContext(_configuration);
        
        services.AddCustomServices();
        services.AddRepositories();
        
        services.RegisterAutoMapper();

        services.ConfigureAzureBlobStorage(_configuration);
        services.AddAzureBlobStorage(_configuration);
        
        services.ConfigureJwt(_configuration);
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(opt => opt
            .AllowAnyHeader()
            .WithExposedHeaders("Content-Disposition")
            .AllowAnyMethod()
            .AllowAnyOrigin()
            .WithExposedHeaders("Token-Expired"));
        
        app.UseContainers();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseMiddleware<CurrentUserMiddleware>();
        
        app.UseFilmRealmContext();
        
        app.UseEndpoints(cfg => { cfg.MapControllers(); });
        
        app.UseHttpsRedirection();
    }
}