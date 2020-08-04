using Microsoft.Extensions.DependencyInjection;
using MGAnonymized.Web.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MGAnonymized.Web.Infrastructure
{
    public class DataInitializerStartupFilter : IStartupTask
    {
        // We need to inject the IServiceProvider so we can create 
        // the scoped service, MyDbContext
        private readonly IServiceProvider _serviceProvider;

        public DataInitializerStartupFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            // Create a new scope to retrieve scoped services
            using (var scope = _serviceProvider.CreateScope())
            {
                // Get the DbContext instance
                var dataInitializerService = scope.ServiceProvider.GetRequiredService<IDataInitializerService>();

                //Do the migration 
                await dataInitializerService.InitializeData();
            }
        }
    }
}