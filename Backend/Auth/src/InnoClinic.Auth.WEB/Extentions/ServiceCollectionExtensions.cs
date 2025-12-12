using InnoClinic.Auth.Application.Data;
using InnoClinic.Auth.Application.Models;
using InnoClinic.Auth.Application.Services;
using InnoClinic.Auth.Services;
using Microsoft.EntityFrameworkCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
        services.Configure<PasswordHashSettings>(configuration.GetSection("PasswordHash"));
        services.Configure<AuthSettings>(configuration.GetSection("Auth"));

        services.AddScoped<IPasswordHashService, PasswordHashService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IAuthService, AuthService>();
            
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
            
        return services;
    }
}