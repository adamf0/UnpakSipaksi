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
using UnpakSipaksi.Modules.Administrasi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Administrasi.Domain.AdministrasiInternal;
using UnpakSipaksi.Modules.Administrasi.Domain.AdministrasiPkm;
using UnpakSipaksi.Modules.Administrasi.Infrastructure.AdministrasiInternal;
using UnpakSipaksi.Modules.Administrasi.Infrastructure.AdministrasiPkm;
using UnpakSipaksi.Modules.Administrasi.Infrastructure.Database;
using UnpakSipaksi.Modules.Administrasi.Presentation.Administrasi;

namespace UnpakSipaksi.Modules.Administrasi.Infrastructure
{
    public static class AdministrasiModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            AdministrasiEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddAdministrasiModule(
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

            services.AddDbContext<AdministrasiInternalDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));
            services.AddDbContext<AdministrasiPkmDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IAdministrasiInternalRepository, AdministrasiInternalRepository>();
            services.AddScoped<IAdministrasiPkmRepository, AdministrasiPkmRepository>();


            services.AddScoped<IUnitOfWorkAdministrasiInternal>(sp => sp.GetRequiredService<AdministrasiInternalDbContext>());
            services.AddScoped<IUnitOfWorkAdministrasiPkm>(sp => sp.GetRequiredService<AdministrasiPkmDbContext>());
        }
    }
}
