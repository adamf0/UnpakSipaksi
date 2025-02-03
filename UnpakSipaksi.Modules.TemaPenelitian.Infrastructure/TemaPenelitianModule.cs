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
using UnpakSipaksi.Modules.TemaPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.TemaPenelitian.Domain.TemaPenelitian;
using UnpakSipaksi.Modules.TemaPenelitian.Infrastructure.TemaPenelitian;
using UnpakSipaksi.Modules.TemaPenelitian.Infrastructure.Database;
using UnpakSipaksi.Modules.TemaPenelitian.Presentation.TemaPenelitian;
using UnpakSipaksi.Modules.TemaPenelitian.PublicApi;
using UnpakSipaksi.Modules.TemaPenelitian.Infrastructure.PublicApi;

namespace UnpakSipaksi.Modules.TemaPenelitian.Infrastructure
{
    public static class TemaPenelitianModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            TemaPenelitianEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddTemaPenelitianModule(
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

            services.AddDbContext<TemaPenelitianDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<ITemaPenelitianRepository, TemaPenelitianRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<TemaPenelitianDbContext>());

            services.AddScoped<ITemaPenelitianApi, TemaPenelitianApi>();
        }
    }
}
