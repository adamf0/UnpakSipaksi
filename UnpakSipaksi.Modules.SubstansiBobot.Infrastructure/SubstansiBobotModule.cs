using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Modules.SubstansiBobot.Presentation.SubstansiBobot;

namespace UnpakSipaksi.Modules.SubstansiBobot.Infrastructure
{
    public static class SubstansiBobotModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            SubstansiBobotEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddSubstansiBobotModule(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddInfrastructure(configuration);

            return services;
        }

        private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

        }
    }
}
