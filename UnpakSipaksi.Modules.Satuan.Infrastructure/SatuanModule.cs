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
using UnpakSipaksi.Modules.Satuan.Domain.Satuan;
using UnpakSipaksi.Modules.Satuan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Satuan.Infrastructure.Satuan;
using UnpakSipaksi.Modules.Satuan.Infrastructure.Database;
using UnpakSipaksi.Modules.Satuan.Presentation.Satuan;
using UnpakSipaksi.Modules.Satuan.PublicApi;
using UnpakSipaksi.Modules.Satuan.Infrastructure.PublicApi;

namespace UnpakSipaksi.Modules.Satuan.Infrastructure
{
    public static class SatuanModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            SatuanEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddSatuanModule(
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

            services.AddDbContext<SatuanDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<ISatuanRepository, SatuanRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<SatuanDbContext>());

            services.AddScoped<ISatuanApi, SatuanApi>();
        }
    }
}
