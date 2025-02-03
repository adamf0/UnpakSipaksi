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
using UnpakSipaksi.Modules.AuthorSinta.Application.Abstractions.Data;
using UnpakSipaksi.Modules.AuthorSinta.Domain.AuthorSinta;
using UnpakSipaksi.Modules.AuthorSinta.Presentation.AuthorSinta;
using UnpakSipaksi.Modules.AuthorSinta.Infrastructure.AuthorSinta;
using UnpakSipaksi.Modules.AuthorSinta.Infrastructure.Database;

namespace UnpakSipaksi.Modules.AuthorSinta.Infrastructure
{
    public static class AuthorSintaModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            AuthorSintaEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddAuthorSintaModule(
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

            services.AddDbContext<AuthorSintaDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IAuthorSintaRepository, AuthorSintaRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AuthorSintaDbContext>());
        }
    }
}
