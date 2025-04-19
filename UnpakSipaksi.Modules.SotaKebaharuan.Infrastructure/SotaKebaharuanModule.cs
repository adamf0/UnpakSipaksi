using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.SotaKebaharuan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.SotaKebaharuan.Domain.SotaKebaharuan;
using UnpakSipaksi.Modules.SotaKebaharuan.Infrastructure.Database;
using UnpakSipaksi.Modules.SotaKebaharuan.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.SotaKebaharuan.Infrastructure.SotaKebaharuan;
using UnpakSipaksi.Modules.SotaKebaharuan.Presentation.SotaKebaharuan;
using UnpakSipaksi.Modules.SotaKebaharuan.PublicApi;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Infrastructure
{
    public static class SotaKebaharuanModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            SotaKebaharuanEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddSotaKebaharuanModule(
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

            services.AddDbContext<SotaKebaharuanDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<ISotaKebaharuanRepository, SotaKebaharuanRepository>();

            services.AddScoped<ISotaKebaharuanApi, SotaKebaharuanApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<SotaKebaharuanDbContext>());
        }
    }
}
