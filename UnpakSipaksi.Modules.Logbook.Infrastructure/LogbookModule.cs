using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.Logbook.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Logbook.Domain.Logbook;
using UnpakSipaksi.Modules.Logbook.Presentation.Logbook;
using UnpakSipaksi.Modules.Logbook.Infrastructure.Database;
using UnpakSipaksi.Modules.Logbook.Infrastructure.Logbook;

namespace UnpakSipaksi.Modules.Logbook.Infrastructure
{
    public static class LogbookModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            LogbookEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddLogbookModule(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddInfrastructure(configuration);

            return services;
        }

        private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string databaseConnectionString = configuration.GetConnectionString("Database")!;

            services.AddScoped<IDbConnectionFactory>(_ => new DbConnectionFactory(databaseConnectionString));

            services.AddDbContext<LogbookDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<ILogbookRepository, LogbookRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<LogbookDbContext>());
        }
    }
}
