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
using UnpakSipaksi.Modules.Referensi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Referensi.Domain.Referensi;
using UnpakSipaksi.Modules.Referensi.Infrastructure.Referensi;
using UnpakSipaksi.Modules.Referensi.Infrastructure.Database;
using UnpakSipaksi.Modules.Referensi.Presentation.Referensi;

namespace UnpakSipaksi.Modules.Referensi.Infrastructure
{
    public static class ReferensiModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            ReferensiEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddReferensiModule(
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

            services.AddDbContext<ReferensiDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IReferensiRepository, ReferensiRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ReferensiDbContext>());
        }
    }
}
