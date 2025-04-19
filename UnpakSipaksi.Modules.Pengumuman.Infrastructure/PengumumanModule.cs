using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.Pengumuman.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Pengumuman.Infrastructure.Database;
using UnpakSipaksi.Modules.Pengumuman.Infrastructure.Pengumuman;
using UnpakSipaksi.Modules.Pengumuman.Presentation.Pengumuman;
using UnpakSipaksi.Modules.Pengumuman.Domain.Pengumuman;

namespace UnpakSipaksi.Modules.Pengumuman.Infrastructure
{
    public static class PengumumanModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            PengumumanEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddPengumumanModule(
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

            services.AddDbContext<PengumumanDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IPengumumanRepository, PengumumanRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<PengumumanDbContext>());
        }
    }
}
