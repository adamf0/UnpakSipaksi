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
using UnpakSipaksi.Modules.JenisLuaran.Application.Abstractions.Data;
using UnpakSipaksi.Modules.JenisLuaran.Domain;
using UnpakSipaksi.Modules.JenisLuaran.Infrastructure.Database;
using UnpakSipaksi.Modules.JenisLuaran.Infrastructure.JenisLuaran;
using UnpakSipaksi.Modules.JenisLuaran.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.JenisLuaran.Presentation.JenisLuaran;
using UnpakSipaksi.Modules.JenisLuaran.PublicApi;

namespace UnpakSipaksi.Modules.JenisLuaran.Infrastructure
{
    public static class JenisLuaranModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            JenisLuaranEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddJenisLuaranModule(
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

            services.AddDbContext<JenisLuaranDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IJenisLuaranRepository, JenisLuaranRepository>();

            services.AddScoped<IJenisLuaranApi, JenisLuaranApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<JenisLuaranDbContext>());
        }
    }
}
