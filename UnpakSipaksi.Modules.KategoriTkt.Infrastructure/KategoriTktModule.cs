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
using UnpakSipaksi.Modules.KategoriTkt.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriTkt.Domain.KategoriTkt;
using UnpakSipaksi.Modules.KategoriTkt.Presentation.KategoriTkt;
using UnpakSipaksi.Modules.KategoriTkt.Infrastructure.Database;
using UnpakSipaksi.Modules.KategoriTkt.Infrastructure.KategoriTkt;

namespace UnpakSipaksi.Modules.KategoriTkt.Infrastructure
{
    public static class KategoriTktModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KategoriTktEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKategoriTktModule(
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

            services.AddDbContext<KategoriTktDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKategoriTktRepository, KategoriTktRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KategoriTktDbContext>());
        }
    }
}
