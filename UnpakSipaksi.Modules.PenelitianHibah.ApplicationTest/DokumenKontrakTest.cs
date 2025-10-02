using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Application.CreateDokumenKontrak;
using UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteDokumenKontrak;
using UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateDokumenKontrak;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenKontrak;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianHibah.ApplicationTest
{
    public class DokumenKontrakTest : BaseIntegrationTest
    {
        public DokumenKontrakTest(IntegrationTestWebAppFactory factory) : base(factory) { }

        public static IEnumerable<object[]> InvalidData()
        {
            var validUuid = Guid.NewGuid().ToString();
            var empty = "";
            var invalidGuid = "invalid-guid";

            // UuidPenelitianHibah
            yield return new object[] { empty, "file.pdf", "'UuidPenelitianHibah' tidak boleh kosong." };
            yield return new object[] { invalidGuid, "file.pdf", "'UuidPenelitianHibah' harus dalam format UUID v4 yang valid." };

            // File
            yield return new object[] { validUuid, "", "'File' tidak boleh kosong." };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateDokumenKontrak_ShouldFailValidation_WhenInvalid(
            string uuidPenelitianHibah,
            string? file,
            string expectedMessage)
        {
            var command = new CreateDokumenKontrakCommand(
                uuidPenelitianHibah,
                file
            );

            var validator = new CreateDokumenKontrakCommandValidator();
            var result = await validator.ValidateAsync(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == expectedMessage);
        }

        [Fact]

        public async Task CreateDokumenKontrak_ShouldBeSuccess_WhenValidData()
        {
            var penelitianHibahId = "1";
            var penelitianHibahUuid = Guid.NewGuid();
            var NIDNBefore = "1234567890";

            var unitOfWork = new Mock<IUnitOfWorkDokumenKontrak>();

            var hibahRepository = new Mock<IPenelitianHibahRepository>();
            hibahRepository.Setup(r => r.HasUniqueDataAsync(
                    It.IsAny<Guid?>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var hibahEntity = Domain.PenelitianHibah.PenelitianHibah
                .Create(hibahRepository.Object, NIDNBefore, "2025-01-01", "Judul")
                .Result.Value;

            typeof(Domain.PenelitianHibah.PenelitianHibah).GetProperty(nameof(Domain.PenelitianHibah.PenelitianHibah.Id))!
                .SetValue(hibahEntity, int.Parse(penelitianHibahId));

            typeof(Domain.PenelitianHibah.PenelitianHibah).GetProperty(nameof(Domain.PenelitianHibah.PenelitianHibah.Uuid))!
                .SetValue(hibahEntity, penelitianHibahUuid);

            hibahRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(hibahEntity);

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var handler = new CreateDokumenKontrakCommandHandler(
                    services.GetRequiredService<IDokumenKontrakRepository>(),
                    hibahRepository.Object,
                    unitOfWork.Object
                );

                var command = new CreateDokumenKontrakCommand(
                    penelitianHibahUuid.ToString(),
                    "file.pdf"
                );

                var result = await handler.Handle(command, CancellationToken.None);
                var dokumenUuid = result.Value.ToString();

                Assert.True(result.IsSuccess);
                //var data = DBContextDokumenKontrak.DokumenKontrak.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == dokumenUuid);
                //Assert.NotNull(data);
                //Assert.Equal("file.pdf", data.File);
            }
        }

        [Fact]
        public async Task UpdateDokumenKontrak_ShouldBeExecute_WhenValidData()
        {
            var penelitianHibahId = "1";
            var penelitianHibahUuid = Guid.NewGuid();
            var NIDNBefore = "1234567890";

            var hibahRepository = new Mock<IPenelitianHibahRepository>();
            hibahRepository.Setup(r => r.HasUniqueDataAsync(
                    It.IsAny<Guid?>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var hibahEntity = Domain.PenelitianHibah.PenelitianHibah
                .Create(hibahRepository.Object, NIDNBefore, "2025-01-01", "Judul")
                .Result.Value;

            typeof(Domain.PenelitianHibah.PenelitianHibah).GetProperty(nameof(Domain.PenelitianHibah.PenelitianHibah.Id))!
                .SetValue(hibahEntity, int.Parse(penelitianHibahId));

            typeof(Domain.PenelitianHibah.PenelitianHibah).GetProperty(nameof(Domain.PenelitianHibah.PenelitianHibah.Uuid))!
                .SetValue(hibahEntity, penelitianHibahUuid);

            hibahRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(hibahEntity);

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dokumenRepo = services.GetRequiredService<IDokumenKontrakRepository>();
                var unitOfWork = services.GetRequiredService<IUnitOfWorkDokumenKontrak>();

                var handler = new CreateDokumenKontrakCommandHandler(
                    services.GetRequiredService<IDokumenKontrakRepository>(),
                    hibahRepository.Object,
                    unitOfWork
                );

                var command = new CreateDokumenKontrakCommand(
                    penelitianHibahUuid.ToString(),
                    "file.pdf"
                );

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result.IsSuccess);
                var dokumenUuid = result.Value.ToString();
                //var data = DBContextDokumenKontrak.DokumenKontrak.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == dokumenUuid);
                //Assert.NotNull(data);
                //Assert.Equal("file.pdf", data.File);

                var updateHandler = new UpdateDokumenKontrakCommandHandler(
                    dokumenRepo,
                    hibahRepository.Object,
                    unitOfWork
                );

                var updateCommand = new UpdateDokumenKontrakCommand(
                    dokumenUuid,
                    penelitianHibahUuid.ToString(),
                    "file1.pdf"
                );

                var updateResult = await updateHandler.Handle(updateCommand, CancellationToken.None);

                // Assert
                Assert.True(updateResult.IsSuccess);
                //var dataUpdate = DBContextDokumenKontrak.DokumenKontrak.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == dokumenUuid);
                //Assert.NotNull(dataUpdate);
                //Assert.Equal("file1.pdf", dataUpdate.File);
            }
        }

        [Fact]
        public async Task DeleteDokumenKontrak_ShouldBeExecute_WhenValidData()
        {
            var penelitianHibahId = "1";
            var penelitianHibahUuid = Guid.NewGuid();
            var NIDNBefore = "1234567890";

            var hibahRepository = new Mock<IPenelitianHibahRepository>();
            hibahRepository.Setup(r => r.HasUniqueDataAsync(
                    It.IsAny<Guid?>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var hibahEntity = Domain.PenelitianHibah.PenelitianHibah
                .Create(hibahRepository.Object, NIDNBefore, "2025-01-01", "Judul")
                .Result.Value;

            typeof(Domain.PenelitianHibah.PenelitianHibah).GetProperty(nameof(Domain.PenelitianHibah.PenelitianHibah.Id))!
                .SetValue(hibahEntity, int.Parse(penelitianHibahId));

            typeof(Domain.PenelitianHibah.PenelitianHibah).GetProperty(nameof(Domain.PenelitianHibah.PenelitianHibah.Uuid))!
                .SetValue(hibahEntity, penelitianHibahUuid);

            hibahRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(hibahEntity);

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dokumenRepo = services.GetRequiredService<IDokumenKontrakRepository>();
                var unitOfWork = services.GetRequiredService<IUnitOfWorkDokumenKontrak>();

                var handler = new CreateDokumenKontrakCommandHandler(
                    services.GetRequiredService<IDokumenKontrakRepository>(),
                    hibahRepository.Object,
                    unitOfWork
                );

                var command = new CreateDokumenKontrakCommand(
                    penelitianHibahUuid.ToString(),
                    "file.pdf"
                );

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result.IsSuccess);
                var dokumenUuid = result.Value.ToString();
                //var data = DBContextDokumenKontrak.DokumenKontrak.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == dokumenUuid);
                //Assert.NotNull(data);
                //Assert.Equal("file.pdf", data.File);

                var deleteHandler = new DeleteDokumenKontrakCommandHandler(
                     dokumenRepo,
                     hibahRepository.Object,
                     unitOfWork
                 );

                var deleteCommand = new DeleteDokumenKontrakCommand(dokumenUuid, penelitianHibahUuid.ToString());
                var deleteResult = await deleteHandler.Handle(deleteCommand, CancellationToken.None);

                Assert.True(deleteResult.IsSuccess);
            }
        }


    }
}
