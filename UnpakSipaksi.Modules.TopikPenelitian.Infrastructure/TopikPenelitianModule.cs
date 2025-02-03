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
using UnpakSipaksi.Modules.TopikPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.TopikPenelitian.Domain.TopikPenelitian;
using UnpakSipaksi.Modules.TopikPenelitian.Presentation.TopikPenelitian;
using UnpakSipaksi.Modules.TopikPenelitian.PublicApi;
using UnpakSipaksi.Modules.TopikPenelitian.Infrastructure.TopikPenelitian;
using UnpakSipaksi.Modules.TopikPenelitian.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.TopikPenelitian.Infrastructure.Database;

namespace UnpakSipaksi.Modules.TopikPenelitian.Infrastructure
{
    public static class TopikPenelitianModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            TopikPenelitianEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddTopikPenelitianModule(
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

            services.AddDbContext<TopikPenelitianDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<ITopikPenelitianRepository, TopikPenelitianRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<TopikPenelitianDbContext>());

            services.AddScoped<ITopikPenelitianApi, TopikPenelitianApi>();
        }
    }
}
