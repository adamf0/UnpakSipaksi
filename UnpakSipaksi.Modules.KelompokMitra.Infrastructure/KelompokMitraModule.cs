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
using UnpakSipaksi.Modules.KelompokMitra.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KelompokMitra.Domain.KelompokMitra;
using UnpakSipaksi.Modules.KelompokMitra.Presentation.KelompokMitra;
using UnpakSipaksi.Modules.KelompokMitra.Infrastructure.KelompokMitra;
using UnpakSipaksi.Modules.KelompokMitra.Infrastructure.Database;
using UnpakSipaksi.Modules.KelompokMitra.PublicApi;
using UnpakSipaksi.Modules.KelompokMitra.Infrastructure.PublicApi;

namespace UnpakSipaksi.Modules.KelompokMitra.Infrastructure
{
    public static class KelompokMitraModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KelompokMitraEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKelompokMitraModule(
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

            services.AddDbContext<KelompokMitraDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKelompokMitraRepository, KelompokMitraRepository>();

            services.AddScoped<IKelompokMitraApi, KelompokMitraApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KelompokMitraDbContext>());
        }
    }
}
