using InnoClinic.Offices.Application.Data;
using InnoClinic.Offices.Application.Services;
using InnoClinic.Offices.WEB.Middleware;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Offices.WEB.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("InnoClinic.Offices.Application")));

        services.AddScoped<IOfficeService, OfficeService>();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        
        return services;
    }
}