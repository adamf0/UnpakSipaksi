using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Modules.JenisPublikasi.Infrastructure.Database;
using Xunit;

namespace UnpakSipaksi.Modules.JenisPublikasi.ApplicationtTest
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
    {
        protected readonly IntegrationTestWebAppFactory Factory;
        private readonly IServiceScope _scope;
        protected readonly ISender Sender;
        protected readonly JenisPublikasiDbContext DBContext;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory) {
            Factory = factory;
            _scope = factory.Services.CreateScope();
            Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
            DBContext = _scope.ServiceProvider.GetRequiredService<JenisPublikasiDbContext>();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
