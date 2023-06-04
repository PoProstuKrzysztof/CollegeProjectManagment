using CollegeProjectManagment.Core.Interfaces;
using CollegeProjectManagment.Infrastructure.Repositories;
using System.Runtime.CompilerServices;

namespace CollegeProjectManagment.DI;

public static class InjectionServices
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        // Services to inject into Program.cs
        services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
    }
}