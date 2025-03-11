using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.KualitasIpteks.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KualitasIpteks.Domain.KualitasIpteks;
using UnpakSipaksi.Modules.KualitasIpteks.Infrastructure.Database;
using UnpakSipaksi.Modules.KualitasIpteks.Infrastructure.KualitasIpteks;
using UnpakSipaksi.Modules.KualitasIpteks.Presentation.KualitasIpteks;

namespace UnpakSipaksi.Modules.KualitasIpteks.Infrastructure
{
    public static class KualitasIpteksModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KualitasIpteksEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKualitasIpteksModule(
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

            services.AddDbContext<KualitasIpteksDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKualitasIpteksRepository, KualitasIpteksRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KualitasIpteksDbContext>());
        }
    }
}
