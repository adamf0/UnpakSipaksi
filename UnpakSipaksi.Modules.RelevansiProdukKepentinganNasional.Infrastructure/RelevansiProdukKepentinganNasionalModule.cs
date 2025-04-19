using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Domain.RelevansiProdukKepentinganNasional;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Infrastructure.Database;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Infrastructure.RelevansiProdukKepentinganNasional;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Presentation.RelevansiProdukKepentinganNasional;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.PublicApi;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Infrastructure
{
    public static class RelevansiProdukKepentinganNasionalModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            RelevansiProdukKepentinganNasionalEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddRelevansiProdukKepentinganNasionalModule(
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

            services.AddDbContext<RelevansiProdukKepentinganNasionalDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IRelevansiProdukKepentinganNasionalRepository, RelevansiProdukKepentinganNasionalRepository>();

            services.AddScoped<IRelevansiProdukKepentinganNasionalApi, RelevansiProdukKepentinganNasionalApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<RelevansiProdukKepentinganNasionalDbContext>());
        }
    }
}
