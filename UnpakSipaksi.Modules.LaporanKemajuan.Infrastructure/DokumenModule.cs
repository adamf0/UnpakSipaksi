using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.LaporanKemajuan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.LaporanKemajuan.Domain.Dokumen;
using UnpakSipaksi.Modules.LaporanKemajuan.Domain.Luaran;
using UnpakSipaksi.Modules.LaporanKemajuan.Infrastructure.Database;
using UnpakSipaksi.Modules.LaporanKemajuan.Infrastructure.Dokumen;
using UnpakSipaksi.Modules.LaporanKemajuan.Infrastructure.Luaran;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Infrastructure
{
    public static class DokumenModule
    {
        //[note] belum selesai coding
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            //DokumenEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddLaporanKemajuanModule(
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

            services.AddDbContext<DokumenHibahContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));
            services.AddDbContext<DokumenPkmContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));
            services.AddDbContext<LuaranHibahContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));
            services.AddDbContext<LuaranPkmContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IDokumenHibahRepository, DokumenHibahRepository>();
            services.AddScoped<IDokumenPkmRepository, DokumenPkmRepository>();
            services.AddScoped<ILuaranHibahRepository, LuaranHibahRepository>();
            services.AddScoped<ILuaranPkmRepository, LuaranPkmRepository>();

            services.AddScoped<IUnitOfWorkDokumenHibah>(sp => sp.GetRequiredService<DokumenHibahContext>());
            services.AddScoped<IUnitOfWorkDokumenPkm>(sp => sp.GetRequiredService<DokumenPkmContext>());
            services.AddScoped<IUnitOfWorkLuaranHibah>(sp => sp.GetRequiredService<LuaranHibahContext>());
            services.AddScoped<IUnitOfWorkLuaranPkm>(sp => sp.GetRequiredService<LuaranPkmContext>());
        }
    }
}
