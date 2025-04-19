using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Domain.KewajaranTahapanTarget;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Infrastructure.Database;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Infrastructure.KewajaranTahapanTarget;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Presentation.KewajaranTahapanTarget;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.PublicApi;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Infrastructure
{
    public static class KewajaranTahapanTargetModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KewajaranTahapanTargetEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKewajaranTahapanTargetModule(
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

            services.AddDbContext<KewajaranTahapanTargetDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKewajaranTahapanTargetRepository, KewajaranTahapanTargetRepository>();

            services.AddScoped<IKewajaranTahapanTargetApi, KewajaranTahapanTargetApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KewajaranTahapanTargetDbContext>());
        }
    }
}
