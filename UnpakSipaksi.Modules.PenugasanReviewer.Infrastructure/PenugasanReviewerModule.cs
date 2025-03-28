using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Infrastructure.Data;
using UnpakSipaksi.Modules.PenugasanReviewer.Domain.PenugasanReviewer;
using UnpakSipaksi.Modules.PenugasanReviewer.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenugasanReviewer.Infrastructure.Database;
using UnpakSipaksi.Modules.PenugasanReviewer.Infrastructure.PenugasanReviewer;
using UnpakSipaksi.Modules.PenugasanReviewer.Presentation.PenugasanReviewer;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Infrastructure
{
    public static class PenugasanReviewerModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            PenugasanReviewerEndpoints.MapEndpoints(app);
        }

        public static IServiceCollection AddPenugasanReviewerModule(
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

            services.AddDbContext<PenugasanReviewerDbContext>(optionsBuilder => optionsBuilder.UseMySQL(databaseConnectionString));

            services.AddScoped<IPenugasanReviewerRepository, PenugasanReviewerRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<PenugasanReviewerDbContext>());
        }
    }
}
