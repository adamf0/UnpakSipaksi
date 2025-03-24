using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Presentation.MetodeRencanaKegiatan;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Domain.MetodeRencanaKegiatan;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Infrastructure.Database;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Infrastructure.MetodeRencanaKegiatan;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Infrastructure
{
    public static class MetodeRencanaKegiatanModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            MetodeRencanaKegiatanEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddMetodeRencanaKegiatanModule(
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

            services.AddDbContext<MetodeRencanaKegiatanDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IMetodeRencanaKegiatanRepository, MetodeRencanaKegiatanRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<MetodeRencanaKegiatanDbContext>());
        }
    }
}
