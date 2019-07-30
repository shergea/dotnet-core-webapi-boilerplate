using Microsoft.EntityFrameworkCore;
using WebApi.DAL;
using WebApi.DAL.Repositories;
using WebApi.DAL.Repositories.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UnitOfWorkServiceProvider
    {
        public static IServiceCollection RegisterUnitOfWorkLayer(this IServiceCollection services, string connection)
        {
            services.AddDbContext<MsSQLContext>(options => options.UseSqlServer(connection));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddDistributedRedisCache(options =>
            {
                options.InstanceName = "localhost";
                options.Configuration = "127.0.0.1";
            });
            return services;
        }
    }
}