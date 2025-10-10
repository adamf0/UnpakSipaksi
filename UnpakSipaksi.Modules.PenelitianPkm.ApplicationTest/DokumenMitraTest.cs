using Microsoft.Extensions.DependencyInjection;
using Moq;
using UnpakSipaksi.Modules.KelompokMitra.PublicApi;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianPkm.Application.CreateDokumenMitra;
using UnpakSipaksi.Modules.PenelitianPkm.Application.DeleteDokumenMitra;
using UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateDokumenMitra;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenMitra;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianPkm.ApplicationTest
{
    public class DokumenMitraTest : BaseIntegrationTest
    {
        public DokumenMitraTest(IntegrationTestWebAppFactory factory) : base(factory) { }

        //public static IEnumerable<object[]> InvalidData()
        //{
        //    var validUuid = Guid.NewGuid().ToString();
        //    var empty = "";
        //    var invalidGuid = "invalid-guid";

        //    // UuidPenelitianPkm
        //    yield return new object[] { empty, "file.pdf", null, "kategori", "'UuidPenelitianPkm' tidak boleh kosong." };
        //    yield return new object[] { invalidGuid, "file.pdf", null, "kategori", "'UuidPenelitianPkm' harus dalam format UUID v4 yang valid." };

        //    // File / Link
        //    yield return new object[] { validUuid, null, null, "kategori", "'File' atau 'Link' harus diisi." };
        //    yield return new object[] { validUuid, "file.pdf", "http://drive.google.com", "kategori", "'File' dan 'Link' tidak boleh diisi bersamaan." };

        //    // Kategori
        //    //yield return new object[] { validUuid, "file.pdf", null, "kategori", "'Kategori' tidak boleh kosong." };
        //}

        //[Theory]
        //[MemberData(nameof(InvalidData))]
        //public async Task CreateDokumenMitra_ShouldFailValidation_WhenInvalid(
        //    string uuidPenelitianPkm,
        //    string? file,
        //    string? link,
        //    string kategori,
        //    string expectedMessage)
        //{
        //    var command = new CreateDokumenMitraCommand(
        //        uuidPenelitianPkm,
        //        file,
        //        link,
        //        kategori
        //    );

        //    var validator = new CreateDokumenMitraCommandValidator();
        //    var result = await validator.ValidateAsync(command);

        //    Assert.False(result.IsValid);
        //    Assert.Contains(result.Errors, e => e.ErrorMessage == expectedMessage);
        //}

        [Fact]
        public async Task CreateDokumenMitra_ShouldBeSuccess_WhenValidData()
        {
            var PenelitianPkmId = "1";
            var PenelitianPkmUuid = Guid.NewGuid();
            var KelompokMitraUuid = Guid.NewGuid();
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

                var kelompokMitraApiMock = new Mock<IKelompokMitraApi>();
                kelompokMitraApiMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new KelompokMitraResponse("1", KelompokMitraUuid.ToString(), "Mitra ABC"));

                var handler = new CreateDokumenMitraCommandHandler(
                    services.GetRequiredService<IDokumenMitraRepository>(),
                    kelompokMitraApiMock.Object,
                    hibahRepository.Object,
                    services.GetRequiredService<IUnitOfWorkDokumenMitra>()
                );

                var command = new CreateDokumenMitraCommand(
                    PenelitianPkmUuid.ToString(),
                    "mitraA",
                    "32",
                    "1111",
                    KelompokMitraUuid.ToString(),
                    "pemimpinMitra",
                    "kontakA",
                    "file.pdf"
                );

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result.IsSuccess);
                //var DokumenMitraUuid = result.Value.ToString();
                //var x = DBContextDokumenMitra.DokumenMitra.AsNoTracking().ToList();

                //var data = DBContextDokumenMitra.DokumenMitra.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == DokumenMitraUuid);
                //Assert.NotNull(data);
                //Assert.Equal("file.pdf", data.File);
                //Assert.Equal("KategoriA", data.Kategori);
            }
        }

        [Fact]
        public async Task UpdateDokumenMitra_ShouldBeExecute_WhenValidData()
        {
            var PenelitianPkmId = "1";
            var PenelitianPkmUuid = Guid.NewGuid();
            var KelompokMitraUuid = Guid.NewGuid();
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

                var kelompokMitraApiMock = new Mock<IKelompokMitraApi>();
                kelompokMitraApiMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new KelompokMitraResponse("1", KelompokMitraUuid.ToString(), "Mitra ABC"));

                var handler = new CreateDokumenMitraCommandHandler(
                    services.GetRequiredService<IDokumenMitraRepository>(),
                    kelompokMitraApiMock.Object,
                    hibahRepository.Object,
                    services.GetRequiredService<IUnitOfWorkDokumenMitra>()
                );

                var command = new CreateDokumenMitraCommand(
                    PenelitianPkmUuid.ToString(),
                    "mitraA",
                    "32",
                    "1111",
                    KelompokMitraUuid.ToString(),
                    "pemimpinMitra",
                    "kontakA",
                    "file.pdf"
                );

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result.IsSuccess);
                var dokumenUuid = result.Value.ToString();
                //var data = DBContextDokumenMitra.DokumenMitra.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == dokumenUuid);
                //Assert.NotNull(data);
                //Assert.Equal("file.pdf", data.File);
                //Assert.Equal("KategoriA", data.Kategori);

                var updateHandler = new UpdateDokumenMitraCommandHandler(
                    services.GetRequiredService<IDokumenMitraRepository>(),
                    kelompokMitraApiMock.Object,
                    hibahRepository.Object,
                    services.GetRequiredService<IUnitOfWorkDokumenMitra>()
                );

                var updateCommand = new UpdateDokumenMitraCommand(
                    dokumenUuid,
                    PenelitianPkmUuid.ToString(),
                    "mitraB",
                    "06",
                    "2222",
                    KelompokMitraUuid.ToString(),
                    "pemimpinMitra",
                    "kontakB",
                    "file1.pdf"
                );

                var updateResult = await updateHandler.Handle(updateCommand, CancellationToken.None);

                // Assert
                Assert.True(updateResult.IsSuccess);
                //var dataUpdate = DBContextDokumenMitra.DokumenMitra.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == dokumenUuid);
                //Assert.NotNull(dataUpdate);
                //Assert.Null(dataUpdate.File);
                //Assert.Equal("http://drive.google.com", dataUpdate.Link);
                //Assert.Equal("KategoriB", dataUpdate.Kategori);
            }
        }

        [Fact]
        public async Task DeleteDokumenMitra_ShouldBeExecute_WhenValidData()
        {
            var PenelitianPkmId = "1";
            var PenelitianPkmUuid = Guid.NewGuid();
            var KelompokMitraUuid = Guid.NewGuid();
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

                var kelompokMitraApiMock = new Mock<IKelompokMitraApi>();
                kelompokMitraApiMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new KelompokMitraResponse("1", KelompokMitraUuid.ToString(), "Mitra ABC"));

                var handler = new CreateDokumenMitraCommandHandler(
                    services.GetRequiredService<IDokumenMitraRepository>(),
                    kelompokMitraApiMock.Object,
                    hibahRepository.Object,
                    services.GetRequiredService<IUnitOfWorkDokumenMitra>()
                );

                var command = new CreateDokumenMitraCommand(
                    PenelitianPkmUuid.ToString(),
                    "mitraA",
                    "32",
                    "1111",
                    KelompokMitraUuid.ToString(),
                    "pemimpinMitra",
                    "kontakA",
                    "file.pdf"
                );

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result.IsSuccess);
                var dokumenUuid = result.Value.ToString();
                //var data = DBContextDokumenMitra.DokumenMitra.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == dokumenUuid);
                //Assert.NotNull(data);
                //Assert.Equal("file.pdf", data.File);
                //Assert.Equal("KategoriA", data.Kategori);

                var deleteHandler = new DeleteDokumenMItraCommandHandler(
                     services.GetRequiredService<IDokumenMitraRepository>(),
                     hibahRepository.Object,
                     services.GetRequiredService<IUnitOfWorkDokumenMitra>()
                 );

                var deleteCommand = new DeleteDokumenMItraCommand(dokumenUuid, PenelitianPkmUuid.ToString());
                var deleteResult = await deleteHandler.Handle(deleteCommand, CancellationToken.None);

                Assert.True(deleteResult.IsSuccess);
            }
        }


    }
}
