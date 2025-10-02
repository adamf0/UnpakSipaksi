using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Modules.KebaruanReferensi.Infrastructure.Database;
using Xunit;

namespace UnpakSipaksi.Modules.KebaruanReferensi.ApplicationTest
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
    {
        protected readonly IntegrationTestWebAppFactory Factory;
        private readonly IServiceScope _scope;
        protected readonly ISender Sender;
        protected readonly KebaruanReferensiDbContext DBContext;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory) {
            Factory = factory;
            _scope = factory.Services.CreateScope();
            Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
            DBContext = _scope.ServiceProvider.GetRequiredService<KebaruanReferensiDbContext>();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
