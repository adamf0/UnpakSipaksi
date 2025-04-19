using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Domain.RelevansiKepakaranTemaProposal;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Infrastructure.Database;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Infrastructure.RelevansiKepakaranTemaProposal;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Presentation.RelevansiKepakaranTemaProposal;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.PublicApi;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Infrastructure
{
    public static class RelevansiKepakaranTemaProposalModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            RelevansiKepakaranTemaProposalEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddRelevansiKepakaranTemaProposalModule(
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

            services.AddDbContext<RelevansiKepakaranTemaProposalDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IRelevansiKepakaranTemaProposalRepository, RelevansiKepakaranTemaProposalRepository>();

            services.AddScoped<IRelevansiKepakaranTemaProposalApi, RelevansiKepakaranTemaProposalApi>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<RelevansiKepakaranTemaProposalDbContext>());
        }
    }
}
