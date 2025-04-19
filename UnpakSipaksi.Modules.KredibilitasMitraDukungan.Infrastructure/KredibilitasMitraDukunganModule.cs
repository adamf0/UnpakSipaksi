using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Domain.KredibilitasMitraDukungan;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Infrastructure.KredibilitasMitraDukungan;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Infrastructure.Database;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Presentation.KredibilitasMitraDukungan;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.PublicApi;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Infrastructure.PublicApi;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Infrastructure
{
    public static class KredibilitasMitraDukunganModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KredibilitasMitraDukunganEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKredibilitasMitraDukunganModule(
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

            services.AddDbContext<KredibilitasMitraDukunganDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKredibilitasMitraDukunganRepository, KredibilitasMitraDukunganRepository>();

            services.AddScoped<IKredibilitasMitraDukunganApi, KredibilitasMitraDukunganApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KredibilitasMitraDukunganDbContext>());
        }
    }
}
