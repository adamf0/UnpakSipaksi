using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Domain.PeningkatanKeberdayaanMitra;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Infrastructure.PeningkatanKeberdayaanMitra;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Infrastructure.Database;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Presentation.PeningkatanKeberdayaanMitra;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Infrastructure
{
    public static class PeningkatanKeberdayaanMitraModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            PeningkatanKeberdayaanMitraEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddPeningkatanKeberdayaanMitraModule(
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

            services.AddDbContext<PeningkatanKeberdayaanMitraDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IPeningkatanKeberdayaanMitraRepository, PeningkatanKeberdayaanMitraRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<PeningkatanKeberdayaanMitraDbContext>());
        }
    }
}
