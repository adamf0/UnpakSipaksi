using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Modules.KelompokRab.PublicApi;
using UnpakSipaksi.Modules.Komponen.PublicApi;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianPkm.Application.CreateRAB;
using UnpakSipaksi.Modules.PenelitianPkm.Application.DeleteRAB;
using UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateRAB;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.RAB;
using UnpakSipaksi.Modules.Satuan.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianPkm.ApplicationTest
{
    public class RABTest : BaseIntegrationTest
    {
        public RABTest(IntegrationTestWebAppFactory factory) : base(factory) { }

        [Fact]
        public async Task CreateRAB_ShouldBeSuccess_WhenValidData()
        {
            var PenelitianPkmUuid = Guid.NewGuid();
            var PenelitianPkmId = 1;

            var hibahRepository = new Mock<IPenelitianPkmRepository>();
            hibahRepository.Setup(r => r.HasUniqueDataAsync(
                    It.IsAny<Guid?>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var hibahEntity = Domain.PenelitianPkm.PenelitianPkm
                .Create(hibahRepository.Object, "1234567890", "2025-01-01", "Judul")
                .Result.Value;

            typeof(Domain.PenelitianPkm.PenelitianPkm)
                .GetProperty(nameof(Domain.PenelitianPkm.PenelitianPkm.Id))!
                .SetValue(hibahEntity, PenelitianPkmId);

            typeof(Domain.PenelitianPkm.PenelitianPkm)
                .GetProperty(nameof(Domain.PenelitianPkm.PenelitianPkm.Uuid))!
                .SetValue(hibahEntity, PenelitianPkmUuid);

            hibahRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(hibahEntity);

            Guid uuidKelompok = Guid.NewGuid();
            Guid uuidKomponen = Guid.NewGuid();
            Guid uuidSatuan = Guid.NewGuid();

            var kelompokApi = new Mock<IKelompokRabApi>();
            kelompokApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(new KelompokRabResponse("10", uuidKelompok.ToString(), "Kelompok A"));

            var komponenApi = new Mock<IKomponenApi>();
            komponenApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(new KomponenResponse("20", uuidKomponen.ToString(), "Komponen A", 10000));

            var satuanApi = new Mock<ISatuanApi>();
            satuanApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new SatuanResponse("30", uuidSatuan.ToString(), "Satuan A"));

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var rabRepository = services.GetRequiredService<IRABRepository>();
                var unitOfWork = services.GetRequiredService<IUnitOfWorkRAB>();

                var handler = new CreateRABCommandHandler(
                rabRepository,
                hibahRepository.Object,
                kelompokApi.Object,
                komponenApi.Object,
                satuanApi.Object,
                unitOfWork
            );

                var command = new CreateRABCommand(
                    PenelitianPkmUuid.ToString(),
                    uuidKelompok.ToString(),
                    uuidKomponen.ToString(),
                    5, // Item
                    uuidSatuan.ToString(),
                    2000, // HargaSatuan
                    10000 // Total = 5 * 2000
                );

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result.IsSuccess);
            }
        }

        [Fact]
        public async Task UpdateRAB_ShouldBeSuccess_WhenValidData()
        {
            // Arrange
            var PenelitianPkmUuid = Guid.NewGuid();
            var PenelitianPkmId = 1;

            var hibahRepository = new Mock<IPenelitianPkmRepository>();
            hibahRepository.Setup(r => r.HasUniqueDataAsync(
                    It.IsAny<Guid?>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var hibahEntity = Domain.PenelitianPkm.PenelitianPkm
                .Create(hibahRepository.Object, "1234567890", "2025-01-01", "Judul")
                .Result.Value;

            typeof(Domain.PenelitianPkm.PenelitianPkm)
                .GetProperty(nameof(Domain.PenelitianPkm.PenelitianPkm.Id))!
                .SetValue(hibahEntity, PenelitianPkmId);

            typeof(Domain.PenelitianPkm.PenelitianPkm)
                .GetProperty(nameof(Domain.PenelitianPkm.PenelitianPkm.Uuid))!
                .SetValue(hibahEntity, PenelitianPkmUuid);

            hibahRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(hibahEntity);

            Guid uuidKelompok = Guid.NewGuid();
            Guid uuidKomponen = Guid.NewGuid();
            Guid uuidSatuan = Guid.NewGuid();

            var kelompokApi = new Mock<IKelompokRabApi>();
            kelompokApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(new KelompokRabResponse("10", uuidKelompok.ToString(), "Kelompok A"));

            var komponenApi = new Mock<IKomponenApi>();
            komponenApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(new KomponenResponse("20", uuidKomponen.ToString(), "Komponen A", 10000));

            var satuanApi = new Mock<ISatuanApi>();
            satuanApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new SatuanResponse("30", uuidSatuan.ToString(), "Satuan A"));

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var rabRepository = services.GetRequiredService<IRABRepository>();
                var unitOfWork = services.GetRequiredService<IUnitOfWorkRAB>();

                var handler = new CreateRABCommandHandler(
                rabRepository,
                hibahRepository.Object,
                kelompokApi.Object,
                komponenApi.Object,
                satuanApi.Object,
                unitOfWork
            );

                var command = new CreateRABCommand(
                    PenelitianPkmUuid.ToString(),
                    uuidKelompok.ToString(),
                    uuidKomponen.ToString(),
                    5, // Item
                    uuidSatuan.ToString(),
                    2000, // HargaSatuan
                    10000 // Total = 5 * 2000
                );

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result.IsSuccess);
                var rabUuid = result.Value.ToString();


                var handlerUpdate = new UpdateRABCommandHandler(
                    rabRepository,
                    hibahRepository.Object,
                    kelompokApi.Object,
                    komponenApi.Object,
                    satuanApi.Object,
                    unitOfWork
                );

                var commandUpdate = new UpdateRABCommand(
                    rabUuid,
                    PenelitianPkmUuid.ToString(),
                    uuidKelompok.ToString(),
                    uuidKomponen.ToString(),
                    20, // Item
                    uuidSatuan.ToString(),
                    2000, // HargaSatuan
                    40000 // Total = 10 * 2000
                );

                var resultUpdate = await handlerUpdate.Handle(commandUpdate, CancellationToken.None);
                Assert.True(resultUpdate.IsSuccess);
            }
        }

        [Fact]
        public async Task DeleteRAB_ShouldBeSuccess_WhenValidData()
        {
            var PenelitianPkmUuid = Guid.NewGuid();
            var PenelitianPkmId = 1;
            
            var hibahRepository = new Mock<IPenelitianPkmRepository>();
            hibahRepository.Setup(r => r.HasUniqueDataAsync(
                    It.IsAny<Guid?>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var hibahEntity = Domain.PenelitianPkm.PenelitianPkm
                .Create(hibahRepository.Object, "1234567890", "2025-01-01", "Judul")
                .Result.Value;

            typeof(Domain.PenelitianPkm.PenelitianPkm)
                .GetProperty(nameof(Domain.PenelitianPkm.PenelitianPkm.Id))!
                .SetValue(hibahEntity, PenelitianPkmId);

            typeof(Domain.PenelitianPkm.PenelitianPkm)
                .GetProperty(nameof(Domain.PenelitianPkm.PenelitianPkm.Uuid))!
                .SetValue(hibahEntity, PenelitianPkmUuid);

            hibahRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(hibahEntity);

            Guid uuidKelompok = Guid.NewGuid();
            Guid uuidKomponen = Guid.NewGuid();
            Guid uuidSatuan = Guid.NewGuid();

            var kelompokApi = new Mock<IKelompokRabApi>();
            kelompokApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(new KelompokRabResponse("10", uuidKelompok.ToString(), "Kelompok A"));

            var komponenApi = new Mock<IKomponenApi>();
            komponenApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(new KomponenResponse("20", uuidKomponen.ToString(), "Komponen A", 10000));

            var satuanApi = new Mock<ISatuanApi>();
            satuanApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new SatuanResponse("30", uuidSatuan.ToString(), "Satuan A"));

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var rabRepository = services.GetRequiredService<IRABRepository>();
                var unitOfWork = services.GetRequiredService<IUnitOfWorkRAB>();

                var handler = new CreateRABCommandHandler(
                rabRepository,
                hibahRepository.Object,
                kelompokApi.Object,
                komponenApi.Object,
                satuanApi.Object,
                unitOfWork
            );

                var command = new CreateRABCommand(
                    PenelitianPkmUuid.ToString(),
                    uuidKelompok.ToString(),
                    uuidKomponen.ToString(),
                    5, // Item
                    uuidSatuan.ToString(),
                    2000, // HargaSatuan
                    10000 // Total = 5 * 2000
                );

                var result = await handler.Handle(command, CancellationToken.None);
                Assert.True(result.IsSuccess);
                var rabUuid = result.Value.ToString();


                var handlerDelete = new DeleteRABCommandHandler(
                    rabRepository,
                    hibahRepository.Object,
                    unitOfWork
                );

                var commandDelete = new DeleteRABCommand(
                    rabUuid,
                    PenelitianPkmUuid.ToString()
                );

                var resultDelete = await handlerDelete.Handle(commandDelete, CancellationToken.None);
                Assert.True(resultDelete.IsSuccess);
            }
        }
    }
}
