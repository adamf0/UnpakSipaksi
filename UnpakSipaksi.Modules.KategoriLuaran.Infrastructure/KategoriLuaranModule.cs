using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.KategoriLuaran.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriLuaran.Domain.Kategori;
using UnpakSipaksi.Modules.KategoriLuaran.Infrastructure.Database;
using UnpakSipaksi.Modules.KategoriLuaran.Infrastructure.KategoriLuaran;
using UnpakSipaksi.Modules.KategoriLuaran.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.KategoriLuaran.Presentation.KategoriLuaran;
using UnpakSipaksi.Modules.KategoriLuaran.PublicApi;

namespace UnpakSipaksi.Modules.KategoriLuaran.Infrastructure
{
    public static class KategoriLuaranModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KategoriLuaranEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKategoriLuaranModule(
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

            services.AddDbContext<KategoriLuaranDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKategoriLuaranRepository, KategoriLuaranRepository>();

            services.AddScoped<IKategoriLuaranApi, KategoriLuaranApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KategoriLuaranDbContext>());
        }
    }
}
