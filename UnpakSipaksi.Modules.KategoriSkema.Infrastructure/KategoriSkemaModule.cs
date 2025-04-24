using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.KategoriSkema.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriSkema.Infrastructure.Database;
using UnpakSipaksi.Modules.KategoriSkema.Presentation.KategoriSkema;
using UnpakSipaksi.Modules.KategoriSkema.Domain.KategoriSkema;
using UnpakSipaksi.Modules.KategoriSkema.Infrastructure.KategoriSkema;
using UnpakSipaksi.Modules.KategoriSkema.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.KategoriSkema.PublicApi;

namespace UnpakSipaksi.Modules.KategoriSkema.Infrastructure
{
    public static class KategoriSkemaModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KategoriSkemaEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKategoriSkemaModule(
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

            services.AddDbContext<KategoriSkemaDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKategoriSkemaRepository, KategoriSkemaRepository>();

            services.AddScoped<IKategoriSkemaApi, KategoriSkemaApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KategoriSkemaDbContext>());
        }
    }
}
