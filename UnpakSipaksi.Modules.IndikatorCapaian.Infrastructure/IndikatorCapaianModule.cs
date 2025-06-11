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
using UnpakSipaksi.Modules.IndikatorCapaian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.IndikatorCapaian.Domain;
using UnpakSipaksi.Modules.IndikatorCapaian.Infrastructure.Database;
using UnpakSipaksi.Modules.IndikatorCapaian.Infrastructure.IndikatorCapaian;
using UnpakSipaksi.Modules.IndikatorCapaian.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.IndikatorCapaian.Presentation.IndikatorCapaian;
using UnpakSipaksi.Modules.IndikatorCapaian.PublicApi;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Infrastructure
{
    public static class IndikatorCapaianModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            IndikatorCapaianEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddIndikatorCapaianModule(
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

            services.AddDbContext<IndikatorCapaianDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IIndikatorCapaianRepository, IndikatorCapaianRepository>();

            services.AddScoped<IIndikatorCapaianApi, IndikatorCapaianApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<IndikatorCapaianDbContext>());
        }
    }
}
