using InnoClinic.Profiles.Application.Data;
using InnoClinic.Profiles.Application.Services;
using InnoClinic.Profiles.WEB.Middleware;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Profiles.WEB.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("InnoClinic.Profiles.Application")));

        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IReceptionistService, ReceptionistService>();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        
        return services;
    }
}