using WebApi.DAL.Repositories;
using WebApi.DAL.Repositories.Interfaces;

namespace Microsoft.Extensions.DependencyInjection {
    public static class UnitOfWorkServiceProvider
    {
        public static IServiceCollection RegisterUnitOfWorkLayer(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}