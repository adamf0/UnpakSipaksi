using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Application.CreateDokumenPendukung;
using UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteDokumenPendukung;
using UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateDokumenPendukung;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenPendukung;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianHibah.ApplicationTest
{
    public class DokumenPendukungTest : BaseIntegrationTest
    {
        public DokumenPendukungTest(IntegrationTestWebAppFactory factory) : base(factory) { }

        public static IEnumerable<object[]> InvalidData()
        {
            var validUuid = Guid.NewGuid().ToString();
            var empty = "";
            var invalidGuid = "invalid-guid";

            // UuidPenelitianHibah
            yield return new object[] { empty, "file.pdf", null, "kategori", "'UuidPenelitianHibah' tidak boleh kosong." };
            yield return new object[] { invalidGuid, "file.pdf", null, "kategori", "'UuidPenelitianHibah' harus dalam format UUID v4 yang valid." };

            // File / Link
            yield return new object[] { validUuid, null, null, "kategori", "'File' atau 'Link' harus diisi." };
            yield return new object[] { validUuid, "file.pdf", "http://drive.google.com", "kategori", "'File' dan 'Link' tidak boleh diisi bersamaan." };

            // Kategori
            //yield return new object[] { validUuid, "file.pdf", null, "kategori", "'Kategori' tidak boleh kosong." };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateDokumenPendukung_ShouldFailValidation_WhenInvalid(
            string uuidPenelitianHibah,
            string? file,
            string? link,
            string kategori,
            string expectedMessage)
        {
            var command = new CreateDokumenPendukungCommand(
                uuidPenelitianHibah,
                file,
                link,
                kategori
            );

            var validator = new CreateDokumenPendukungCommandValidator();
            var result = await validator.ValidateAsync(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage == expectedMessage);
        }

        [Fact]
        public async Task CreateDokumenPendukung_ShouldBeSuccess_WhenValidData()
        {
            var penelitianHibahId = "1";
            var penelitianHibahUuid = Guid.NewGuid();
            var NIDNBefore = "1234567890";

            var unitOfWork = new Mock<IUnitOfWorkDokumenPendukung>();

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

                var handler = new CreateDokumenPendukungCommandHandler(
                    services.GetRequiredService<IDokumenPendukungRepository>(),
                    hibahRepository.Object,
                    unitOfWork.Object
                );

                var command = new CreateDokumenPendukungCommand(
                    penelitianHibahUuid.ToString(),
                    "file.pdf",
                    null,
                    "KategoriA"
                );

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result.IsSuccess);
                //var dokumenPendukungUuid = result.Value.ToString();
                //var x = DBContextDokumenPendukung.DokumenPendukung.AsNoTracking().ToList();

                //var data = DBContextDokumenPendukung.DokumenPendukung.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == dokumenPendukungUuid);
                //Assert.NotNull(data);
                //Assert.Equal("file.pdf", data.File);
                //Assert.Equal("KategoriA", data.Kategori);
            }
        }

        [Fact]
        public async Task UpdateDokumenPendukung_ShouldBeExecute_WhenValidData()
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
                var dokumenRepo = services.GetRequiredService<IDokumenPendukungRepository>();
                var unitOfWork = services.GetRequiredService<IUnitOfWorkDokumenPendukung>();

                var handler = new CreateDokumenPendukungCommandHandler(
                    services.GetRequiredService<IDokumenPendukungRepository>(),
                    hibahRepository.Object,
                    unitOfWork
                );

                var command = new CreateDokumenPendukungCommand(
                    penelitianHibahUuid.ToString(),
                    "file.pdf",
                    null,
                    "KategoriA"
                );

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result.IsSuccess);
                var dokumenUuid = result.Value.ToString();
                //var data = DBContextDokumenPendukung.DokumenPendukung.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == dokumenUuid);
                //Assert.NotNull(data);
                //Assert.Equal("file.pdf", data.File);
                //Assert.Equal("KategoriA", data.Kategori);

                var updateHandler = new UpdateDokumenPendukungCommandHandler(
                    dokumenRepo,
                    hibahRepository.Object,
                    unitOfWork
                );

                var updateCommand = new UpdateDokumenPendukungCommand(
                    dokumenUuid,
                    penelitianHibahUuid.ToString(),
                    null, // hapus file
                    "http://drive.google.com", // ubah ke link
                    "KategoriB"
                );

                var updateResult = await updateHandler.Handle(updateCommand, CancellationToken.None);

                // Assert
                Assert.True(updateResult.IsSuccess);
                //var dataUpdate = DBContextDokumenPendukung.DokumenPendukung.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == dokumenUuid);
                //Assert.NotNull(dataUpdate);
                //Assert.Null(dataUpdate.File);
                //Assert.Equal("http://drive.google.com", dataUpdate.Link);
                //Assert.Equal("KategoriB", dataUpdate.Kategori);
            }
        }

        [Fact]
        public async Task DeleteDokumenPendukung_ShouldBeExecute_WhenValidData()
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
                var dokumenRepo = services.GetRequiredService<IDokumenPendukungRepository>();
                var unitOfWork = services.GetRequiredService<IUnitOfWorkDokumenPendukung>();

                var handler = new CreateDokumenPendukungCommandHandler(
                    services.GetRequiredService<IDokumenPendukungRepository>(),
                    hibahRepository.Object,
                    unitOfWork
                );

                var command = new CreateDokumenPendukungCommand(
                    penelitianHibahUuid.ToString(),
                    "file.pdf",
                    null,
                    "KategoriA"
                );

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result.IsSuccess);
                var dokumenUuid = result.Value.ToString();
                //var data = DBContextDokumenPendukung.DokumenPendukung.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == dokumenUuid);
                //Assert.NotNull(data);
                //Assert.Equal("file.pdf", data.File);
                //Assert.Equal("KategoriA", data.Kategori);

                var deleteHandler = new DeleteDokumenPendukungCommandHandler(
                     dokumenRepo,
                     hibahRepository.Object,
                     unitOfWork
                 );

                var deleteCommand = new DeleteDokumenPendukungCommand(dokumenUuid, penelitianHibahUuid.ToString());
                var deleteResult = await deleteHandler.Handle(deleteCommand, CancellationToken.None);

                Assert.True(deleteResult.IsSuccess);
            }
        }


    }
}
