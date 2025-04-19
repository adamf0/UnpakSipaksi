using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.LuaranArtikel.Application.Abstractions.Data;
using UnpakSipaksi.Modules.LuaranArtikel.Domain.LuaranArtikel;
using UnpakSipaksi.Modules.LuaranArtikel.Infrastructure.Database;
using UnpakSipaksi.Modules.LuaranArtikel.Infrastructure.LuaranArtikel;
using UnpakSipaksi.Modules.LuaranArtikel.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.LuaranArtikel.Presentation.LuaranArtikel;
using UnpakSipaksi.Modules.LuaranArtikel.PublicApi;

namespace UnpakSipaksi.Modules.LuaranArtikel.Infrastructure
{
    public static class LuaranArtikelModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            LuaranArtikelEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddLuaranArtikelModule(
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

            services.AddDbContext<LuaranArtikelDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<ILuaranArtikelRepository, LuaranArtikelRepository>();

            services.AddScoped<ILuaranArtikelApi, LuaranArtikelApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<LuaranArtikelDbContext>());
        }
    }
}
