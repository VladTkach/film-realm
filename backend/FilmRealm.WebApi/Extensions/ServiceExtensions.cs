using System.Security.Claims;
using System.Text;
using Azure.Core.Extensions;
using Azure.Storage.Blobs;
using FilmRealm.BLL.Interfaces;
using FilmRealm.BLL.Services;
using FilmRealm.BLL.Sieve;
using FilmRealm.BlobStorage.Interfaces;
using FilmRealm.BlobStorage.Models;
using FilmRealm.BlobStorage.Services;
using FilmRealm.Common.Interfaces;
using FilmRealm.Common.JWT;
using FilmRealm.DAL.Context;
using FilmRealm.DAL.Interfaces;
using FilmRealm.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.IdentityModel.Tokens;
using Sieve.Services;

namespace FilmRealm.WebApi.Extensions;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<JwtIssuerOptions>();
        services.AddScoped<IJwtFactory, JwtFactory>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IActorService, ActorService>();
        services.AddScoped<IFilmService, FilmService>();
        services.AddScoped<ICommentService, CommentService>();

        services.AddScoped<IImageService, ImageService>();
        
        services.AddScoped<UserIdStorageService>();
        services.AddTransient<IUserIdSetter>(s => s.GetRequiredService<UserIdStorageService>());
        services.AddTransient<IUserIdGetter>(s => s.GetRequiredService<UserIdStorageService>());

        services.AddScoped<ISieveProcessor, CustomSieveProcessor>();
        services.AddScoped<ISieveCustomFilterMethods, SieveCustomFilterMethods>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IActorRepository, ActorRepository>();
        services.AddScoped<IFilmRepository, FilmRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
    }

    public static void AddFilmRealmContext(this IServiceCollection services, IConfiguration configuration)
    {
        var migrationAssembly = typeof(FilmRealmContext).Assembly.GetName().Name;
        services.AddDbContext<FilmRealmContext>(options =>
            options.UseSqlServer(configuration["ConnectionStrings:FilmRealmDBConnection"],
                opt => opt.MigrationsAssembly(migrationAssembly)));
    }

    public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions))!;
        // Get secret key from appSettings for testing.
        var secretKey = jwtAppSettingOptions[nameof(JwtIssuerOptions.SecretJwtKey)];
        var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey!));

        // Configure JwtIssuerOptions
        services.Configure<JwtIssuerOptions>(options =>
        {
            options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)]!;
            options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)]!;
            options.SecretJwtKey = secretKey!;
            options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            options.Roles = new List<string> {"User", "Admin"};
        });

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

            ValidateAudience = true,
            ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,

            RequireExpirationTime = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            
            RoleClaimType = ClaimTypes.Role
        };

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(configureOptions =>
        {
            configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
            configureOptions.TokenValidationParameters = tokenValidationParameters;
            configureOptions.SaveToken = true;

            configureOptions.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add("Token-Expired", "true");
                    }

                    return Task.CompletedTask;
                }
            };
        });
    }
    
    public static void AddAzureBlobStorage(this IServiceCollection services, IConfiguration configuration)
    {            
        services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddBlobServiceClient(configuration.GetConnectionString("BlobStorageConnectionString")!, preferMsi: true);
        });
        services.AddTransient<IBlobService, BlobService>();
    }

    private static IAzureClientBuilder<BlobServiceClient, BlobClientOptions> AddBlobServiceClient(this AzureClientFactoryBuilder builder, string serviceUriOrConnectionString, bool preferMsi)
    {
        if (preferMsi && Uri.TryCreate(serviceUriOrConnectionString, UriKind.Absolute, out Uri? serviceUri))
        {
            return builder.AddBlobServiceClient(serviceUri)!;
        }
        return builder.AddBlobServiceClient(serviceUriOrConnectionString)!;
    }
    
    public static void ConfigureAzureBlobStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BlobStorageOptions>(configuration.GetSection(nameof(BlobStorageOptions)));
    }
}