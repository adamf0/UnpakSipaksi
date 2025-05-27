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
using UnpakSipaksi.Modules.Kategori.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Kategori.Domain.Kategori;
using UnpakSipaksi.Modules.Kategori.Infrastructure.Database;
using UnpakSipaksi.Modules.Kategori.Infrastructure.Kategori;
using UnpakSipaksi.Modules.Kategori.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.Kategori.Presentation.Kategori;
using UnpakSipaksi.Modules.Kategori.PublicApi;

namespace UnpakSipaksi.Modules.Kategori.Infrastructure
{
    public static class KategoriModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KategoriEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKategoriModule(
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

            services.AddDbContext<KategoriDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKategoriRepository, KategoriRepository>();

            services.AddScoped<IKategoriApi, KategoriApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KategoriDbContext>());
        }
    }
}
