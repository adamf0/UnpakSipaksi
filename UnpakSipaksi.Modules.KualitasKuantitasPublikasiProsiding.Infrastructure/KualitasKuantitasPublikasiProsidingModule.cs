using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Domain.KualitasKuantitasPublikasiProsiding;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Infrastructure.Database;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Infrastructure.KualitasKuantitasPublikasiProsiding;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Presentation.KualitasKuantitasPublikasiProsiding;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.PubliApi;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Infrastructure
{
    public static class KualitasKuantitasPublikasiProsidingModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KualitasKuantitasPublikasiProsidingEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKualitasKuantitasPublikasiProsidingModule(
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

            services.AddDbContext<KualitasKuantitasPublikasiProsidingDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKualitasKuantitasPublikasiProsidingRepository, KualitasKuantitasPublikasiProsidingRepository>();

            services.AddScoped<IKualitasKuantitasPublikasiProsidingApi, KualitasKuantitasPublikasiProsidingApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KualitasKuantitasPublikasiProsidingDbContext>());
        }
    }
}
