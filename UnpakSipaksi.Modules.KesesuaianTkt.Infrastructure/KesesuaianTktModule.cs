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
using UnpakSipaksi.Modules.KesesuaianTkt.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KesesuaianTkt.Domain.KesesuaianTkt;
using UnpakSipaksi.Modules.KesesuaianTkt.Presentation.KesesuaianTkt;
using UnpakSipaksi.Modules.KesesuaianTkt.Infrastructure.KesesuaianTkt;
using UnpakSipaksi.Modules.KesesuaianTkt.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Infrastructure
{
    public static class KesesuaianTktModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KesesuaianTktEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKesesuaianTktModule(
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

            services.AddDbContext<KesesuaianTktDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKesesuaianTktRepository, KesesuaianTktRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KesesuaianTktDbContext>());
        }
    }
}
