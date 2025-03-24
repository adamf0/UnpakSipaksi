using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Domain.PotensiKetercapaianLuaranDijanjikan;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Presentation.PotensiKetercapaianLuaranDijanjikan;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Infrastructure.PotensiKetercapaianLuaranDijanjikan;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Infrastructure.Database;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Infrastructure
{
    public static class PotensiKetercapaianLuaranDijanjikanModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            PotensiKetercapaianLuaranDijanjikanEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddPotensiKetercapaianLuaranDijanjikanModule(
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

            services.AddDbContext<PotensiKetercapaianLuaranDijanjikanDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IPotensiKetercapaianLuaranDijanjikanRepository, PotensiKetercapaianLuaranDijanjikanRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<PotensiKetercapaianLuaranDijanjikanDbContext>());
        }
    }
}
