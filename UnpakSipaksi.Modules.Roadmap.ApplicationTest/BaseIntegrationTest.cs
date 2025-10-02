using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Modules.Roadmap.Infrastructure.Database;
using Xunit;

namespace UnpakSipaksi.Modules.Roadmap.ApplicationTest
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
    {
        protected readonly IntegrationTestWebAppFactory Factory;
        private readonly IServiceScope _scope;
        protected readonly ISender Sender;
        protected readonly RoadmapDbContext DBContext;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory) {
            Factory = factory;
            _scope = factory.Services.CreateScope();
            Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
            DBContext = _scope.ServiceProvider.GetRequiredService<RoadmapDbContext>();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
