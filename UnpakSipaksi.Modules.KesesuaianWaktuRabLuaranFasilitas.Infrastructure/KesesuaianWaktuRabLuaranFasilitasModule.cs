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
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Domain.KesesuaianWaktuRabLuaranFasilitas;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Presentation.KesesuaianWaktuRabLuaranFasilitas;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Infrastructure.KesesuaianWaktuRabLuaranFasilitas;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Infrastructure.Database;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.PublicApi;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Infrastructure.PublicApi;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Infrastructure
{
    public static class KesesuaianWaktuRabLuaranFasilitasModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KesesuaianWaktuRabLuaranFasilitasEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKesesuaianWaktuRabLuaranFasilitasModule(
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

            services.AddDbContext<KesesuaianWaktuRabLuaranFasilitasDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKesesuaianWaktuRabLuaranFasilitasRepository, KesesuaianWaktuRabLuaranFasilitasRepository>();

            services.AddScoped<IKesesuaianWaktuRabLuaranFasilitasApi, KesesuaianWaktuRabLuaranFasilitasApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KesesuaianWaktuRabLuaranFasilitasDbContext>());
        }
    }
}
