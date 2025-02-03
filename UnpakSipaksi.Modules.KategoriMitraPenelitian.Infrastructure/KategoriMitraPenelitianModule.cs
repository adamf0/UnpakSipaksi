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
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Domain.KategoriMitraPenelitian;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Infrastructure.KategoriMitraPenelitian;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Infrastructure.Database;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Presentation.KategoriMitraPenelitian;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Infrastructure
{
    public static class KategoriMitraPenelitianModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KategoriMitraPenelitianEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKategoriMitraPenelitianModule(
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

            services.AddDbContext<KategoriMitraPenelitianDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKategoriMitraPenelitianRepository, KategoriMitraPenelitianRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KategoriMitraPenelitianDbContext>());
        }
    }
}
