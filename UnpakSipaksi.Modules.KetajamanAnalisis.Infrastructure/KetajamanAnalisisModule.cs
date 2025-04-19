using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.KetajamanAnalisis.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KetajamanAnalisis.Domain.KetajamanAnalisis;
using UnpakSipaksi.Modules.KetajamanAnalisis.Infrastructure.KetajamanAnalisis;
using UnpakSipaksi.Modules.KetajamanAnalisis.Infrastructure.Database;
using UnpakSipaksi.Modules.KetajamanAnalisis.Presentation.KetajamanAnalisis;
using UnpakSipaksi.Modules.KetajamanAnalisis.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.KetajamanAnalisis.PublicApi;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Infrastructure
{
    public static class KetajamanAnalisisModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KetajamanAnalisisEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKetajamanAnalisisModule(
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

            services.AddDbContext<KetajamanAnalisisDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKetajamanAnalisisRepository, KetajamanAnalisisRepository>();

            services.AddScoped<IKetajamanAnalisisApi, KetajamanAnalisisApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KetajamanAnalisisDbContext>());
        }
    }
}
