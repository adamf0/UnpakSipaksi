using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Domain.KetajamanPerumusanMasalah;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Infrastructure.KetajamanPerumusanMasalah;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Infrastructure.Database;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Presentation.KetajamanPerumusanMasalah;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Infrastructure
{
    public static class KetajamanPerumusanMasalahModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KetajamanPerumusanMasalahEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKetajamanPerumusanMasalahModule(
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

            services.AddDbContext<KetajamanPerumusanMasalahDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKetajamanPerumusanMasalahRepository, KetajamanPerumusanMasalahRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KetajamanPerumusanMasalahDbContext>());
        }
    }
}
