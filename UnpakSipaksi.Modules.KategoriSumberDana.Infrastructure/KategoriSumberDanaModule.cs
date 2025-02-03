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
using UnpakSipaksi.Modules.KategoriSumberDana.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriSumberDana.Domain.KategoriSumberDana;
using UnpakSipaksi.Modules.KategoriSumberDana.Presentation.KategoriSumberDana;
using UnpakSipaksi.Modules.KategoriSumberDana.Infrastructure.Database;
using UnpakSipaksi.Modules.KategoriSumberDana.Infrastructure.KategoriSumberDana;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Infrastructure
{
    public static class KategoriSumberDanaModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KategoriSumberDanaEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKategoriSumberDanaModule(
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

            services.AddDbContext<KategoriSumberDanaDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKategoriSumberDanaRepository, KategoriSumberDanaRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KategoriSumberDanaDbContext>());
        }
    }
}
