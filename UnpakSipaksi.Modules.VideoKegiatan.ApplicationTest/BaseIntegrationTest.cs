using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Modules.VideoKegiatan.Infrastructure.Database;
using Xunit;

namespace UnpakSipaksi.Modules.VideoKegiatan.ApplicationTest
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
    {
        protected readonly IntegrationTestWebAppFactory Factory;
        private readonly IServiceScope _scope;
        protected readonly ISender Sender;
        protected readonly VideoKegiatanDbContext DBContext;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory) {
            Factory = factory;
            _scope = factory.Services.CreateScope();
            Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
            DBContext = _scope.ServiceProvider.GetRequiredService<VideoKegiatanDbContext>();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
