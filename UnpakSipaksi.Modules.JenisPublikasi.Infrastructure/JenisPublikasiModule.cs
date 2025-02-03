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
using UnpakSipaksi.Modules.JenisPublikasi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.JenisPublikasi.Domain.JenisPublikasi;
using UnpakSipaksi.Modules.JenisPublikasi.Infrastructure.JenisPublikasi;
using UnpakSipaksi.Modules.JenisPublikasi.Infrastructure.Database;
using UnpakSipaksi.Modules.JenisPublikasi.Presentation.JenisPublikasi;

namespace UnpakSipaksi.Modules.JenisPublikasi.Infrastructure
{
    public static class JenisPublikasiModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            JenisPublikasiEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddJenisPublikasiModule(
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

            services.AddDbContext<JenisPublikasiDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IJenisPublikasiRepository, JenisPublikasiRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<JenisPublikasiDbContext>());
        }
    }
}
