using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Modules.AkurasiPenelitian.Infrastructure.Database;
using Xunit;

namespace Application.Integration.Tests
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
    {
        protected readonly IntegrationTestWebAppFactory Factory;
        private readonly IServiceScope _scope;
        protected readonly ISender Sender;
        protected readonly AkurasiPenelitianDbContext DBContext;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory) {
            Factory = factory;
            _scope = factory.Services.CreateScope();
            Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
            DBContext = _scope.ServiceProvider.GetRequiredService<AkurasiPenelitianDbContext>();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
