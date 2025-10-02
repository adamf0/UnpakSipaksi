using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Modules.RumpunIlmu3.Infrastructure.Database;
using Xunit;

namespace UnpakSipaksi.Modules.RumpunIlmu3.ApplicationTest
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
    {
        protected readonly IntegrationTestWebAppFactory Factory;
        private readonly IServiceScope _scope;
        protected readonly ISender Sender;
        protected readonly RumpunIlmu3DbContext DBContext;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory) {
            Factory = factory;
            _scope = factory.Services.CreateScope();
            Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
            DBContext = _scope.ServiceProvider.GetRequiredService<RumpunIlmu3DbContext>();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
