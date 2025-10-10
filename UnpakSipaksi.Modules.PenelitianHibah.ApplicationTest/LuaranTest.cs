using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Kategori.PublicApi;
using UnpakSipaksi.Modules.KategoriLuaran.PublicApi;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Application.CreateLuaran;
using UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteLuaran;
using UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateLuaran;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianHibah.ApplicationTest
{
    public class LuaranTest : BaseIntegrationTest
    {
        public LuaranTest(IntegrationTestWebAppFactory factory) : base(factory) { }

        //public static IEnumerable<object[]> InvalidData()
        //{
        //    var validUuid = Guid.NewGuid().ToString();
        //    var empty = "";
        //    var invalidGuid = "invalid-guid";

        //    // CREATE: uuidPenelitian, uuidKategori, uuidKategoriLuaran, jenis
        //    yield return new object[] { empty, empty, empty, empty, "wajib", "'UuidPenelitianHibah' tidak boleh kosong.", "created" };
        //    yield return new object[] { empty, invalidGuid, validUuid, validUuid, "wajib", "'UuidPenelitianHibah' harus dalam format UUID v4 yang valid.", "created" };
        //    yield return new object[] { empty, validUuid, empty, validUuid, "wajib", "'UuidKategori' tidak boleh kosong.", "created" };
        //    yield return new object[] { empty, validUuid, invalidGuid, validUuid, "wajib", "'UuidKategori' harus dalam format UUID v4 yang valid.", "created" };
        //    yield return new object[] { empty, validUuid, validUuid, empty, "wajib", "'UuidKategoriLuaran' tidak boleh kosong.", "created" };
        //    yield return new object[] { empty, validUuid, validUuid, invalidGuid, "wajib", "'UuidKategoriLuaran' harus dalam format UUID v4 yang valid.", "created" };
        //    yield return new object[] { empty, validUuid, validUuid, validUuid, empty, "'Jenis' tidak boleh kosong.", "created" };
        //    yield return new object[] { empty, validUuid, validUuid, validUuid, "invalid", "'Jenis' harus bernilai 'wajib' atau 'tambahan'.", "created" };

        //    // UPDATE: uuid, uuidPenelitian, uuidKategori, uuidKategoriLuaran, jenis
        //    yield return new object[] { empty, validUuid, validUuid, validUuid, "wajib", "'Uuid' tidak boleh kosong.", "updated" };
        //    yield return new object[] { invalidGuid, validUuid, validUuid, validUuid, "wajib", "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };
        //    yield return new object[] { validUuid, empty, validUuid, validUuid, "wajib", "'UuidPenelitianHibah' tidak boleh kosong.", "updated" };
        //    yield return new object[] { validUuid, invalidGuid, validUuid, validUuid, "wajib", "'UuidPenelitianHibah' harus dalam format UUID v4 yang valid.", "updated" };
        //    yield return new object[] { validUuid, validUuid, empty, validUuid, "wajib", "'UuidKategori' tidak boleh kosong.", "updated" };
        //    yield return new object[] { validUuid, validUuid, invalidGuid, validUuid, "wajib", "'UuidKategori' harus dalam format UUID v4 yang valid.", "updated" };
        //    yield return new object[] { validUuid, validUuid, validUuid, empty, "wajib", "'UuidKategoriLuaran' tidak boleh kosong.", "updated" };
        //    yield return new object[] { validUuid, validUuid, validUuid, invalidGuid, "wajib", "'UuidKategoriLuaran' harus dalam format UUID v4 yang valid.", "updated" };
        //    yield return new object[] { validUuid, validUuid, validUuid, validUuid, empty, "'Jenis' tidak boleh kosong.", "updated" };
        //    yield return new object[] { validUuid, validUuid, validUuid, validUuid, "invalid", "'Jenis' harus bernilai 'wajib' atau 'tambahan'.", "updated" };

        //    // DELETE: hanya uuid
        //    yield return new object[] { empty, empty, empty, empty, empty, "'Uuid' tidak boleh kosong.", "deleted" };
        //    yield return new object[] { invalidGuid, empty, empty, empty, empty, "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        //}

        //[Theory]
        //[MemberData(nameof(InvalidData))]
        //public async Task LuaranCommand_ShouldThrow_WhenInvalid(
        //    string uuid,
        //    string uuidPenelitian,
        //    string uuidKategori,
        //    string uuidKategoriLuaran,
        //    string jenis,
        //    string expectedMessage,
        //    string mode)
        //{
        //    Result? result = null;

        //    if (mode == "created")
        //    {
        //        var command = new CreateLuaranCommand(
        //            uuidPenelitian,
        //            uuidKategori,
        //            uuidKategoriLuaran,
        //            "keterangan",
        //            "http://link.com",
        //            jenis
        //        );
        //        result = await Sender.Send(command);
        //    }
        //    else if (mode == "updated")
        //    {
        //        var command = new UpdateLuaranCommand(
        //            uuid,
        //            uuidPenelitian,
        //            uuidKategori,
        //            uuidKategoriLuaran,
        //            "keterangan",
        //            "http://link.com",
        //            jenis
        //        );
        //        result = await Sender.Send(command);
        //    }
        //    else // deleted
        //    {
        //        var command = new DeleteLuaranCommand(uuid, uuidPenelitian);
        //        result = await Sender.Send(command);
        //    }

        //    Assert.True(result.IsFailure);
        //    if (result.Error is ValidationError validationError)
        //    {
        //        Assert.Contains(validationError.Errors, e => e.Description == expectedMessage);
        //    }
        //    else
        //    {
        //        Assert.Equal(expectedMessage, result.Error.Description);
        //    }
        //}


        [Fact]
        public async Task Create_ShouldBeSuccess_WhenValidData()
        {
            // arrange
            var penelitianHibahId = "1";
            var penelitianHibahUuid = Guid.NewGuid();
            var NIDNBefore = "1234567890";

            var kategoriUuid = Guid.NewGuid();
            var kategoriId = "10";
            var kategoriLuaranUuid = Guid.NewGuid();
            var kategoriLuaranId = "20";

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

            var kategoriApi = new Mock<IKategoriApi>();
            kategoriApi.Setup(r => r.GetAsync(kategoriUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new KategoriResponse(kategoriId, kategoriUuid.ToString(), "Kategori X"));

            var kategoriLuaranApi = new Mock<IKategoriLuaranApi>();
            kategoriLuaranApi.Setup(r => r.GetAsync(kategoriLuaranUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new KategoriLuaranResponse(kategoriLuaranId, kategoriLuaranUuid.ToString(), kategoriUuid.ToString(), "10", "Luaran X", "aktif"));

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var handler = new CreateLuaranCommandHandler(
                    services.GetRequiredService<Domain.Luaran.ILuaranRepository>(),
                    hibahRepository.Object,
                    kategoriApi.Object,
                    kategoriLuaranApi.Object,
                    services.GetRequiredService<IUnitOfWorkLuaran>());

                var command = new CreateLuaranCommand(
                    penelitianHibahUuid.ToString(),
                    kategoriUuid.ToString(),
                    kategoriLuaranUuid.ToString(),
                    "keterangan awal",
                    "https://drive.google.com/1",
                    "wajib"
                );

                // act
                Result<Guid> result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result.IsSuccess);
                var memberUuid = result.Value.ToString();

                //var data = DBContextLuaran.Luaran.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == memberUuid);
                //Assert.NotNull(data);
                //Assert.Equal(int.Parse(kategoriId), data.KategoriId);
                //Assert.Equal(int.Parse(kategoriLuaranId), data.LuaranId);
            }
        }

        [Fact]
        public async Task Update_ShouldBeExecute_WhenValidData()
        {
            // arrange
            var penelitianHibahId = "1";
            var penelitianHibahUuid = Guid.NewGuid();
            var kategoriUuid = Guid.NewGuid();
            var kategoriId = "10";
            var kategoriLuaranUuid = Guid.NewGuid();
            var kategoriLuaranId = "20";

            var hibahRepository = new Mock<IPenelitianHibahRepository>();
            hibahRepository.Setup(r => r.HasUniqueDataAsync(
                    It.IsAny<Guid?>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var hibahEntity = Domain.PenelitianHibah.PenelitianHibah
                .Create(hibahRepository.Object, "1234567890", "2025-01-01", "Judul")
                .Result.Value;

            typeof(Domain.PenelitianHibah.PenelitianHibah).GetProperty(nameof(Domain.PenelitianHibah.PenelitianHibah.Id))!
                .SetValue(hibahEntity, int.Parse(penelitianHibahId));

            typeof(Domain.PenelitianHibah.PenelitianHibah).GetProperty(nameof(Domain.PenelitianHibah.PenelitianHibah.Uuid))!
                .SetValue(hibahEntity, penelitianHibahUuid);

            hibahRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(hibahEntity);

            var kategoriApi = new Mock<IKategoriApi>();
            kategoriApi.Setup(r => r.GetAsync(kategoriUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new KategoriResponse(kategoriId, kategoriUuid.ToString(), "Kategori X"));

            var kategoriLuaranApi = new Mock<IKategoriLuaranApi>();
            kategoriLuaranApi.Setup(r => r.GetAsync(kategoriLuaranUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new KategoriLuaranResponse(kategoriLuaranId, kategoriLuaranUuid.ToString(), kategoriUuid.ToString(), "10", "Luaran X", "aktif"));

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                // === CREATE ===
                var createHandler = new CreateLuaranCommandHandler(
                    services.GetRequiredService<Domain.Luaran.ILuaranRepository>(),
                    hibahRepository.Object,
                    kategoriApi.Object,
                    kategoriLuaranApi.Object,
                    services.GetRequiredService<IUnitOfWorkLuaran>());

                var createCommand = new CreateLuaranCommand(
                    penelitianHibahUuid.ToString(),
                    kategoriUuid.ToString(),
                    kategoriLuaranUuid.ToString(),
                    "keterangan awal",
                    "https://drive.google.com/1",
                    "wajib"
                );

                Result<Guid> result = await createHandler.Handle(createCommand, CancellationToken.None);
                Assert.True(result.IsSuccess);
                var luaranUuid = result.Value.ToString();

                //var data = DBContextLuaran.Luaran.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == luaranUuid);
                //Assert.NotNull(data);

                // === UPDATE ===
                var updateHandler = new UpdateLuaranCommandHandler(
                    services.GetRequiredService<Domain.Luaran.ILuaranRepository>(),
                    hibahRepository.Object,
                    kategoriApi.Object,
                    kategoriLuaranApi.Object,
                    services.GetRequiredService<IUnitOfWorkLuaran>());

                var updateCommand = new UpdateLuaranCommand(
                    luaranUuid,
                    penelitianHibahUuid.ToString(),
                    kategoriUuid.ToString(),
                    kategoriLuaranUuid.ToString(),
                    "keterangan update",
                    "https://drive.google.com/2",
                    "tambahan"
                );

                var updateResult = await updateHandler.Handle(updateCommand, default);
                Assert.True(updateResult.IsSuccess);

                //var updated = DBContextLuaran.Luaran.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == luaranUuid);
                //Assert.NotNull(updated);
                //Assert.Equal("keterangan update", updated.Keterangan);
                //Assert.Equal("https://drive.google.com/2", updated.Link);
                //Assert.Equal("tambahan", updated.Jenis);
            }
        }

        [Fact]
        public async Task Delete_ShouldBeExecute_WhenValidData()
        {
            // arrange
            var penelitianHibahId = "1";
            var penelitianHibahUuid = Guid.NewGuid();
            var kategoriUuid = Guid.NewGuid();
            var kategoriId = "10";
            var kategoriLuaranUuid = Guid.NewGuid();
            var kategoriLuaranId = "20";

            var hibahRepository = new Mock<IPenelitianHibahRepository>();
            hibahRepository.Setup(r => r.HasUniqueDataAsync(
                    It.IsAny<Guid?>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var hibahEntity = Domain.PenelitianHibah.PenelitianHibah
                .Create(hibahRepository.Object, "1234567890", "2025-01-01", "Judul")
                .Result.Value;

            typeof(Domain.PenelitianHibah.PenelitianHibah).GetProperty(nameof(Domain.PenelitianHibah.PenelitianHibah.Id))!
                .SetValue(hibahEntity, int.Parse(penelitianHibahId));

            typeof(Domain.PenelitianHibah.PenelitianHibah).GetProperty(nameof(Domain.PenelitianHibah.PenelitianHibah.Uuid))!
                .SetValue(hibahEntity, penelitianHibahUuid);

            hibahRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(hibahEntity);

            var kategoriApi = new Mock<IKategoriApi>();
            kategoriApi.Setup(r => r.GetAsync(kategoriUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new KategoriResponse(kategoriId, kategoriUuid.ToString(), "Kategori X"));

            var kategoriLuaranApi = new Mock<IKategoriLuaranApi>();
            kategoriLuaranApi.Setup(r => r.GetAsync(kategoriLuaranUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new KategoriLuaranResponse(kategoriLuaranId, kategoriLuaranUuid.ToString(), kategoriUuid.ToString(), "10", "Luaran X", "aktif"));

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                // === CREATE ===
                var createHandler = new CreateLuaranCommandHandler(
                    services.GetRequiredService<Domain.Luaran.ILuaranRepository>(),
                    hibahRepository.Object,
                    kategoriApi.Object,
                    kategoriLuaranApi.Object,
                    services.GetRequiredService<IUnitOfWorkLuaran>());

                var createCommand = new CreateLuaranCommand(
                    penelitianHibahUuid.ToString(),
                    kategoriUuid.ToString(),
                    kategoriLuaranUuid.ToString(),
                    "keterangan awal",
                    "http://link.com",
                    "wajib"
                );

                Result<Guid> result = await createHandler.Handle(createCommand, CancellationToken.None);
                Assert.True(result.IsSuccess);
                var luaranUuid = result.Value.ToString();

                //var data = DBContextLuaran.Luaran.FirstOrDefault(p => p.Uuid.ToString() == luaranUuid);
                //Assert.NotNull(data);

                // === DELETE ===
                var deleteHandler = new DeleteLuaranCommandHandler(
                    services.GetRequiredService<Domain.Luaran.ILuaranRepository>(),
                    hibahRepository.Object,
                    services.GetRequiredService<IUnitOfWorkLuaran>());

                var deleteCommand = new DeleteLuaranCommand(luaranUuid, penelitianHibahUuid.ToString());
                var deleteResult = await deleteHandler.Handle(deleteCommand, default);

                Assert.True(deleteResult.IsSuccess);

                //var deleted = DBContextLuaran.Luaran.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == luaranUuid);
                //Assert.Null(deleted);
            }
        }

    }
}
