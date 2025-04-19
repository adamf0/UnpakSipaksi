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
using UnpakSipaksi.Modules.KebaruanReferensi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KebaruanReferensi.Domain.KebaruanReferensi;
using UnpakSipaksi.Modules.KebaruanReferensi.Presentation.KebaruanReferensi;
using UnpakSipaksi.Modules.KebaruanReferensi.Infrastructure.Database;
using UnpakSipaksi.Modules.KebaruanReferensi.Infrastructure.KebaruanReferensi;
using UnpakSipaksi.Modules.KebaruanReferensi.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.KebaruanReferensi.PublicApi;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Infrastructure
{
    public static class KebaruanReferensiModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KebaruanReferensiEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKebaruanReferensiModule(
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

            services.AddDbContext<KebaruanReferensiDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKebaruanReferensiRepository, KebaruanReferensiRepository>();

            services.AddScoped<IKebaruanReferensiApi, KebaruanReferensiApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KebaruanReferensiDbContext>());
        }
    }
}
