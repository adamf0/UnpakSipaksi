using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.Abstractions.Data;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Domain.ArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Infrastructure.ArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Infrastructure.Database;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Presentation.ArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.PublicApi;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Infrastructure
{
    public static class ArtikelMediaMassaModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            ArtikelMediaMassaEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddArtikelMediaMassaModule(
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

            services.AddDbContext<ArtikelMediaMassaDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IArtikelMediaMassaRepository, ArtikelMediaMassaRepository>();

            services.AddScoped<IArtikelMediaMassaApi, ArtikelMediaMassaApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ArtikelMediaMassaDbContext>());
        }
    }
}
