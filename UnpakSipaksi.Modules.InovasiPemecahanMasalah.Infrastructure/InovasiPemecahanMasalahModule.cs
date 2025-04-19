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
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Domain.InovasiPemecahanMasalah;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Presentation.InovasiPemecahanMasalah;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Infrastructure.Database;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Infrastructure.InovasiPemecahanMasalah;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.PublicApi;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Infrastructure.PublicApi;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Infrastructure
{
    public static class InovasiPemecahanMasalahModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            InovasiPemecahanMasalahEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddInovasiPemecahanMasalahModule(
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

            services.AddDbContext<InovasiPemecahanMasalahDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IInovasiPemecahanMasalahRepository, InovasiPemecahanMasalahRepository>();

            services.AddScoped<IInovasiPemecahanMasalahApi, InovasiPemecahanMasalahApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<InovasiPemecahanMasalahDbContext>());
        }
    }
}
