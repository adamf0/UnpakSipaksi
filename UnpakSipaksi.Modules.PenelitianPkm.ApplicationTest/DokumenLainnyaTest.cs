using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianPkm.Application.CreateDokumenLainnya;
using UnpakSipaksi.Modules.PenelitianPkm.Application.DeleteDokumenLainnya;
using UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateDokumenLainnya;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenLainnya;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianPkm.ApplicationTest
{
    public class DokumenLainnyaTest : BaseIntegrationTest
    {
        public DokumenLainnyaTest(IntegrationTestWebAppFactory factory) : base(factory) { }

        //public static IEnumerable<object[]> InvalidData()
        //{
        //    var validUuid = Guid.NewGuid().ToString();
        //    var empty = "";
        //    var invalidGuid = "invalid-guid";

        //    // UuidPenelitianPkm
        //    yield return new object[] { empty, "file.pdf", "'UuidPenelitianPkm' tidak boleh kosong." };
        //    yield return new object[] { invalidGuid, "file.pdf", "'UuidPenelitianPkm' harus dalam format UUID v4 yang valid." };

        //    // File
        //    yield return new object[] { validUuid, "", "'File' tidak boleh kosong." };
        //}

        //[Theory]
        //[MemberData(nameof(InvalidData))]
        //public async Task CreateDokumenLainnya_ShouldFailValidation_WhenInvalid(
        //    string uuidPenelitianPkm,
        //    string? file,
        //    string expectedMessage)
        //{
        //    var command = new CreateDokumenLainnyaCommand(
        //        uuidPenelitianPkm,
        //        file
        //    );

        //    var validator = new CreateDokumenLainnyaCommandValidator();
        //    var result = await validator.ValidateAsync(command);

        //    Assert.False(result.IsValid);
        //    Assert.Contains(result.Errors, e => e.ErrorMessage == expectedMessage);
        //}

        [Fact]

        public async Task CreateDokumenLainnya_ShouldBeSuccess_WhenValidData()
        {
            var PenelitianPkmId = "1";
            var PenelitianPkmUuid = Guid.NewGuid();
            var NIDNBefore = "1234567890";

            var unitOfWork = new Mock<IUnitOfWorkDokumenLainnya>();

            var hibahRepository = new Mock<IPenelitianPkmRepository>();
            hibahRepository.Setup(r => r.HasUniqueDataAsync(
                    It.IsAny<Guid?>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var hibahEntity = Domain.PenelitianPkm.PenelitianPkm
                .Create(hibahRepository.Object, NIDNBefore, "2025-01-01", "Judul")
                .Result.Value;

            typeof(Domain.PenelitianPkm.PenelitianPkm).GetProperty(nameof(Domain.PenelitianPkm.PenelitianPkm.Id))!
                .SetValue(hibahEntity, int.Parse(PenelitianPkmId));

            typeof(Domain.PenelitianPkm.PenelitianPkm).GetProperty(nameof(Domain.PenelitianPkm.PenelitianPkm.Uuid))!
                .SetValue(hibahEntity, PenelitianPkmUuid);

            hibahRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(hibahEntity);

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var handler = new CreateDokumenLainnyaCommandHandler(
                    services.GetRequiredService<IDokumenLainnyaRepository>(),
                    hibahRepository.Object,
                    unitOfWork.Object
                );

                var command = new CreateDokumenLainnyaCommand(
                    PenelitianPkmUuid.ToString(),
                    "file.pdf",
                    "kategoriA"
                );

                var result = await handler.Handle(command, CancellationToken.None);
                var dokumenUuid = result.Value.ToString();

                Assert.True(result.IsSuccess);
                //var data = DBContextDokumenLainnya.DokumenLainnya.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == dokumenUuid);
                //Assert.NotNull(data);
                //Assert.Equal("file.pdf", data.File);
            }
        }

        [Fact]
        public async Task UpdateDokumenLainnya_ShouldBeExecute_WhenValidData()
        {
            var PenelitianPkmId = "1";
            var PenelitianPkmUuid = Guid.NewGuid();
            var NIDNBefore = "1234567890";

            var hibahRepository = new Mock<IPenelitianPkmRepository>();
            hibahRepository.Setup(r => r.HasUniqueDataAsync(
                    It.IsAny<Guid?>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var hibahEntity = Domain.PenelitianPkm.PenelitianPkm
                .Create(hibahRepository.Object, NIDNBefore, "2025-01-01", "Judul")
                .Result.Value;

            typeof(Domain.PenelitianPkm.PenelitianPkm).GetProperty(nameof(Domain.PenelitianPkm.PenelitianPkm.Id))!
                .SetValue(hibahEntity, int.Parse(PenelitianPkmId));

            typeof(Domain.PenelitianPkm.PenelitianPkm).GetProperty(nameof(Domain.PenelitianPkm.PenelitianPkm.Uuid))!
                .SetValue(hibahEntity, PenelitianPkmUuid);

            hibahRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(hibahEntity);

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dokumenRepo = services.GetRequiredService<IDokumenLainnyaRepository>();
                var unitOfWork = services.GetRequiredService<IUnitOfWorkDokumenLainnya>();

                var handler = new CreateDokumenLainnyaCommandHandler(
                    services.GetRequiredService<IDokumenLainnyaRepository>(),
                    hibahRepository.Object,
                    unitOfWork
                );

                var command = new CreateDokumenLainnyaCommand(
                    PenelitianPkmUuid.ToString(),
                    "file.pdf",
                    "kategoria"
                );

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result.IsSuccess);
                var dokumenUuid = result.Value.ToString();
                //var data = DBContextDokumenLainnya.DokumenLainnya.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == dokumenUuid);
                //Assert.NotNull(data);
                //Assert.Equal("file.pdf", data.File);

                var updateHandler = new UpdateDokumenLainnyaCommandHandler(
                    dokumenRepo,
                    hibahRepository.Object,
                    unitOfWork
                );

                var updateCommand = new UpdateDokumenLainnyaCommand(
                    dokumenUuid,
                    PenelitianPkmUuid.ToString(),
                    "file1.pdf",
                    "kategoriB"
                );

                var updateResult = await updateHandler.Handle(updateCommand, CancellationToken.None);

                // Assert
                Assert.True(updateResult.IsSuccess);
                //var dataUpdate = DBContextDokumenLainnya.DokumenLainnya.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == dokumenUuid);
                //Assert.NotNull(dataUpdate);
                //Assert.Equal("file1.pdf", dataUpdate.File);
            }
        }

        [Fact]
        public async Task DeleteDokumenLainnya_ShouldBeExecute_WhenValidData()
        {
            var PenelitianPkmId = "1";
            var PenelitianPkmUuid = Guid.NewGuid();
            var NIDNBefore = "1234567890";

            var hibahRepository = new Mock<IPenelitianPkmRepository>();
            hibahRepository.Setup(r => r.HasUniqueDataAsync(
                    It.IsAny<Guid?>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var hibahEntity = Domain.PenelitianPkm.PenelitianPkm
                .Create(hibahRepository.Object, NIDNBefore, "2025-01-01", "Judul")
                .Result.Value;

            typeof(Domain.PenelitianPkm.PenelitianPkm).GetProperty(nameof(Domain.PenelitianPkm.PenelitianPkm.Id))!
                .SetValue(hibahEntity, int.Parse(PenelitianPkmId));

            typeof(Domain.PenelitianPkm.PenelitianPkm).GetProperty(nameof(Domain.PenelitianPkm.PenelitianPkm.Uuid))!
                .SetValue(hibahEntity, PenelitianPkmUuid);

            hibahRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(hibahEntity);

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dokumenRepo = services.GetRequiredService<IDokumenLainnyaRepository>();
                var unitOfWork = services.GetRequiredService<IUnitOfWorkDokumenLainnya>();

                var handler = new CreateDokumenLainnyaCommandHandler(
                    services.GetRequiredService<IDokumenLainnyaRepository>(),
                    hibahRepository.Object,
                    unitOfWork
                );

                var command = new CreateDokumenLainnyaCommand(
                    PenelitianPkmUuid.ToString(),
                    "file.pdf",
                    "kategoriA"
                );

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result.IsSuccess);
                var dokumenUuid = result.Value.ToString();
                //var data = DBContextDokumenLainnya.DokumenLainnya.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == dokumenUuid);
                //Assert.NotNull(data);
                //Assert.Equal("file.pdf", data.File);

                var deleteHandler = new DeleteDokumenLainnyaCommandHandler(
                     dokumenRepo,
                     hibahRepository.Object,
                     unitOfWork
                 );

                var deleteCommand = new DeleteDokumenLainnyaCommand(dokumenUuid, PenelitianPkmUuid.ToString());
                var deleteResult = await deleteHandler.Handle(deleteCommand, CancellationToken.None);

                Assert.True(deleteResult.IsSuccess);
            }
        }


    }
}
