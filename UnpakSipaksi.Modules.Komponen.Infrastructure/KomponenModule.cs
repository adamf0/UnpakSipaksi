using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.Komponen.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Komponen.Domain.Komponen;
using UnpakSipaksi.Modules.Komponen.Presentation.Komponen;
using UnpakSipaksi.Modules.Komponen.Infrastructure.Database;
using UnpakSipaksi.Modules.Komponen.Infrastructure.Komponen;

namespace UnpakSipaksi.Modules.Komponen.Infrastructure
{
    public static class KomponenModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KomponenEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKomponenModule(
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

            services.AddDbContext<KomponenContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKomponenRepository, KomponenRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KomponenContext>());
        }
    }
}
