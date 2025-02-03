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
using UnpakSipaksi.Modules.FokusPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.FokusPenelitian.Domain.FokusPenelitian;
using UnpakSipaksi.Modules.FokusPenelitian.Presentation.FokusPenelitian;
using UnpakSipaksi.Modules.FokusPenelitian.Infrastructure.FokusPenelitian;
using UnpakSipaksi.Modules.FokusPenelitian.Infrastructure.Database;
using UnpakSipaksi.Modules.FokusPenelitian.PublicApi;
using UnpakSipaksi.Modules.FokusPenelitian.Infrastructure.PublicApi;

namespace UnpakSipaksi.Modules.FokusPenelitian.Infrastructure
{
    public static class FokusPenelitianModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            FokusPenelitianEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddFokusPenelitianModule(
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

            services.AddDbContext<FokusPenelitianDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IFokusPenelitianRepository, FokusPenelitianRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<FokusPenelitianDbContext>());

            services.AddScoped<IFokusPenelitianApi, FokusPenelitianApi>();
        }
    }
}
