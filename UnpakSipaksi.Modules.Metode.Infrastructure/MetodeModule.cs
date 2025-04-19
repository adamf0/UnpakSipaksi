using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.Metode.Domain.Metode;
using UnpakSipaksi.Modules.Metode.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Metode.Presentation.Metode;
using UnpakSipaksi.Modules.Metode.Infrastructure.Metode;
using UnpakSipaksi.Modules.Metode.Infrastructure.Database;

namespace UnpakSipaksi.Modules.Metode.Infrastructure
{
    public static class MetodeModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            MetodeEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddMetodeModule(
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

            services.AddDbContext<MetodeDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IMetodeRepository, MetodeRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<MetodeDbContext>());
        }
    }
}
