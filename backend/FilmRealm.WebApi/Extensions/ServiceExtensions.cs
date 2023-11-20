using System.Security.Claims;
using System.Text;
using FilmRealm.BLL.Interfaces;
using FilmRealm.BLL.Services;
using FilmRealm.Common.Interfaces;
using FilmRealm.Common.JWT;
using FilmRealm.DAL.Context;
using FilmRealm.DAL.Interfaces;
using FilmRealm.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FilmRealm.WebApi.Extensions;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<JwtIssuerOptions>();
        services.AddScoped<IJwtFactory, JwtFactory>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
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
        // Get secret key from appsettings for testing.
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
}