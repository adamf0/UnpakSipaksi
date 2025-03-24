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
using UnpakSipaksi.Modules.PrioritasRiset.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PrioritasRiset.Domain.PrioritasRiset;
using UnpakSipaksi.Modules.PrioritasRiset.Presentation.PrioritasRiset;
using UnpakSipaksi.Modules.PrioritasRiset.Infrastructure.PrioritasRiset;
using UnpakSipaksi.Modules.PrioritasRiset.Infrastructure.Database;

namespace UnpakSipaksi.Modules.PrioritasRiset.Infrastructure
{
    public static class PrioritasRisetModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            PrioritasRisetEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddPrioritasRisetModule(
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

            services.AddDbContext<PrioritasRisetDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IPrioritasRisetRepository, PrioritasRisetRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<PrioritasRisetDbContext>());
        }
    }
}
