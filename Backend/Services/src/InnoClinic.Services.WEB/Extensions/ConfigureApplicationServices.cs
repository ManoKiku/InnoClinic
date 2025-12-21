using InnoClinic.Services.Application.Data;
using InnoClinic.Services.Application.Services;
using InnoClinic.Services.WEB.Middleware;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Services.WEB.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("InnoClinic.Services.Application")));

        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IServicesService, ServicesService>();
        services.AddScoped<ISpecializationService, SpecializationService>();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        
        return services;
    }
}