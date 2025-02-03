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
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Domain.KesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Presentation.KesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Infrastructure.KesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Infrastructure.Database;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Infrastructure
{
    public static class KesesuaianPenugasanModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KesesuaianPenugasanEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKesesuaianPenugasanModule(
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

            services.AddDbContext<KesesuaianPenugasanDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKesesuaianPenugasanRepository, KesesuaianPenugasanRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KesesuaianPenugasanDbContext>());
        }
    }
}
