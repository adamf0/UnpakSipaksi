using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Domain.KualitasKuantitasPublikasiJurnalIlmiah;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Infrastructure.Database;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Infrastructure.KualitasKuantitasPublikasiJurnalIlmiah;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Presentation.KualitasKuantitasPublikasiJurnalIlmiah;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.PubliApi;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Infrastructure
{
    public static class KualitasKuantitasPublikasiJurnalIlmiahModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KualitasKuantitasPublikasiJurnalIlmiahEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKualitasKuantitasPublikasiJurnalIlmiahModule(
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

            services.AddDbContext<KualitasKuantitasPublikasiJurnalIlmiahDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKualitasKuantitasPublikasiJurnalIlmiahRepository, KualitasKuantitasPublikasiJurnalIlmiahRepository>();

            services.AddScoped<IKualitasKuantitasPublikasiJurnalIlmiahApi, KualitasKuantitasPublikasiJurnalIlmiahApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KualitasKuantitasPublikasiJurnalIlmiahDbContext>());
        }
    }
}
