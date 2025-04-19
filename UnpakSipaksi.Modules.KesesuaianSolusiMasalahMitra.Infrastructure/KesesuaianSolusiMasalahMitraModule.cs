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
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Domain.KesesuaianSolusiMasalahMitra;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Presentation.KesesuaianSolusiMasalahMitra;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Infrastructure.Database;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Infrastructure.KesesuaianSolusiMasalahMitra;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.PublicApi;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Infrastructure.PublicApi;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Infrastructure
{
    public static class KesesuaianSolusiMasalahMitraModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KesesuaianSolusiMasalahMitraEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKesesuaianSolusiMasalahMitraModule(
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

            services.AddDbContext<KesesuaianSolusiMasalahMitraDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKesesuaianSolusiMasalahMitraRepository, KesesuaianSolusiMasalahMitraRepository>();

            services.AddScoped<IKesesuaianSolusiMasalahMitraApi, KesesuaianSolusiMasalahMitraApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KesesuaianSolusiMasalahMitraDbContext>());
        }
    }
}
