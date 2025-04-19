using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Domain.RelevansiKualitasReferensi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Infrastructure.Database;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Infrastructure.RelevansiKualitasReferensi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Presentation.RelevansiKualitasReferensi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.PublicApi;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Infrastructure
{
    public static class RelevansiKualitasReferensiModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            RelevansiKualitasReferensiEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddRelevansiKualitasReferensiModule(
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

            services.AddDbContext<RelevansiKualitasReferensiDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IRelevansiKualitasReferensiRepository, RelevansiKualitasReferensiRepository>();

            services.AddScoped<IRelevansiKualitasReferensiApi, RelevansiKualitasReferensiApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<RelevansiKualitasReferensiDbContext>());
        }
    }
}
