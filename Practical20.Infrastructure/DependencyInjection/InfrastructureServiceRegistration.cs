using Practical20.Infrastructure.Data.DbContext;

namespace Practical20.Infrastructure.DependencyInjection;

// Added this extentsion method on IServiceCollection to register all the services related to infrastructure layer in one place.
// This will be called in the Program.cs file of the API project to add these services to the DI container.
// Also this make program.cs file clean and maintainable by keeping the infrastructure related service registrations in the
// infrastructure layer itself.
public static class InfrastructureServiceRegistration
{
    /// <summary>
    /// This method registers following services for the infrastructure layer:-
    /// => DbContext
    /// => Unit of Work
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<StudentDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddHttpContextAccessor();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
