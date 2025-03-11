using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.KuantitasStatusKi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KuantitasStatusKi.Domain.KuantitasStatusKi;
using UnpakSipaksi.Modules.KuantitasStatusKi.Infrastructure.Database;
using UnpakSipaksi.Modules.KuantitasStatusKi.Infrastructure.KuantitasStatusKi;
using UnpakSipaksi.Modules.KuantitasStatusKi.Presentation.KuantitasStatusKi;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Infrastructure
{
    public static class KuantitasStatusKiModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KuantitasStatusKiEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKuantitasStatusKiModule(
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

            services.AddDbContext<KuantitasStatusKiDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKuantitasStatusKiRepository, KuantitasStatusKiRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KuantitasStatusKiDbContext>());
        }
    }
}
