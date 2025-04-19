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
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KesesuaianJadwal.Domain.KesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Presentation.KesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Infrastructure.KesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Infrastructure.Database;
using UnpakSipaksi.Modules.KesesuaianJadwal.PublicApi;
using UnpakSipaksi.Modules.KesesuaianJadwal.Infrastructure.PublicApi;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Infrastructure
{
    public static class KesesuaianJadwalModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KesesuaianJadwalEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKesesuaianJadwalModule(
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

            services.AddDbContext<KesesuaianJadwalDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKesesuaianJadwalRepository, KesesuaianJadwalRepository>();

            services.AddScoped<IKesesuaianJadwalApi, KesesuaianJadwalApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KesesuaianJadwalDbContext>());
        }
    }
}
