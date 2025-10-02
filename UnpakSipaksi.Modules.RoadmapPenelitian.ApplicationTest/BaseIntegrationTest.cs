using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Modules.RoadmapPenelitian.Infrastructure.Database;
using Xunit;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.ApplicationTest
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
    {
        protected readonly IntegrationTestWebAppFactory Factory;
        private readonly IServiceScope _scope;
        protected readonly ISender Sender;
        protected readonly RoadmapPenelitianDbContext DBContext;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory) {
            Factory = factory;
            _scope = factory.Services.CreateScope();
            Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
            DBContext = _scope.ServiceProvider.GetRequiredService<RoadmapPenelitianDbContext>();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
