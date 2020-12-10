using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Notification.Infrastructure.DataBase;
using Notification.Infrastructure.Repository;
using Notification.Application.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Notification.Infrastructure.Configuration;

namespace Notification.DI
{
    public class DependencyResolver
    {
        private IConfiguration Configuration { get; }

        public DependencyResolver(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceCollection Resolve(IServiceCollection services)
        {
            services ??= new ServiceCollection();

            var connectionString = Configuration.GetConnectionString("NotificationDbContext");
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);

            services.Configure<AzureServiceBusConfig>(options => Configuration.GetSection(nameof(AzureServiceBusConfig)).Bind(options));

            services.AddSingleton<EventDispatcher>();
            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
