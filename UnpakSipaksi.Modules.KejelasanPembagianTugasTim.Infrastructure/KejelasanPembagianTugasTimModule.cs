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
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Domain.KejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Presentation.KejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Infrastructure.KejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Infrastructure.Database;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.PublicApi;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Infrastructure.PublicApi;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Infrastructure
{
    public static class KejelasanPembagianTugasTimModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            KejelasanPembagianTugasTimEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddKejelasanPembagianTugasTimModule(
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

            services.AddDbContext<KejelasanPembagianTugasTimDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IKejelasanPembagianTugasTimRepository, KejelasanPembagianTugasTimRepository>();

            services.AddScoped<IKejelasanPembagianTugasTimApi, KejelasanPembagianTugasTimApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<KejelasanPembagianTugasTimDbContext>());
        }
    }
}
