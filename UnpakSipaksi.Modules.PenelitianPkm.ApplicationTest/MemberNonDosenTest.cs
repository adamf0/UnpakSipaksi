using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianPkm.Application.CreateMemberNonDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Application.DeleteMemberNonDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateMemberNonDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberNonDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianPkm.ApplicationTest
{
    public class MemberNonDosenTest : BaseIntegrationTest
    {
        public MemberNonDosenTest(IntegrationTestWebAppFactory factory) : base(factory) { }

        //public static IEnumerable<object[]> InvalidData()
        //{
        //    var valid = Guid.NewGuid().ToString();
        //    var empty = "";

        //    // CREATE
        //    yield return new object[] { empty, empty, "'UuidPenelitianPkm' tidak boleh kosong.", "created" };
        //    yield return new object[] { empty, "no-guid", "'UuidPenelitianPkm' harus dalam format UUID v4 yang valid.", "created" };

        //    // UPDATE
        //    yield return new object[] { empty, valid, "'Uuid' tidak boleh kosong.", "updated" };
        //    yield return new object[] { "no-guid", valid, "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };
        //    yield return new object[] { valid, empty, "'UuidPenelitianPkm' tidak boleh kosong.", "updated" };
        //    yield return new object[] { valid, "no-guid", "'UuidPenelitianPkm' harus dalam format UUID v4 yang valid.", "updated" };

        //    // DELETE
        //    yield return new object[] { empty, valid, "'Uuid' tidak boleh kosong.", "deleted" };
        //    yield return new object[] { "no-guid", valid, "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        //}

        //[Theory]
        //[MemberData(nameof(InvalidData))]
        //public async Task CreateUpdateDelete_ShouldThrow_WhenInvalidFluentValidation(
        //    string uuid,
        //    string UuidPenelitianPkm,
        //    string message,
        //    string mode)
        //{
        //    Result? result = null;

        //    if (mode == "created")
        //    {
        //        var command = new CreateMemberNonDosenCommand(UuidPenelitianPkm, "", "Nama", "Afiliasi");
        //        result = await Sender.Send(command);
        //    }
        //    else if (mode == "updated")
        //    {
        //        var command = new UpdateMemberNonDosenCommand(uuid, UuidPenelitianPkm, "", "Nama Baru", "Afiliasi Baru");
        //        result = await Sender.Send(command);
        //    }
        //    else
        //    {
        //        var command = new DeleteMemberNonDosenCommand(uuid);
        //        result = await Sender.Send(command);
        //    }

        //    Assert.True(result.IsFailure);
        //    if (result.Error is ValidationError validationError)
        //    {
        //        Assert.Contains(validationError.Errors, e => e.Description == message);
        //    }
        //    else
        //    {
        //        Assert.Equal(message, result.Error.Description);
        //    }
        //}

        [Fact]
        public async Task Create_ShouldBeExecute_WhenValidData()
        {
            //arrange
            var PenelitianPkmId = "1";
            var PenelitianPkmUuid = Guid.NewGuid();
            var nomorIdentitas = "987654321";
            var nama = "Non Dosen Test";
            var afiliasi = "External";
            var judul = "uji coba";
            var tahun = "2025-01-01";

            //act
            var PenelitianPkmApi = new Mock<IPenelitianPkmApi>();
            PenelitianPkmApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PenelitianPkmResponse(PenelitianPkmId, PenelitianPkmUuid.ToString(), null, judul, tahun, null, null, null, "draf", null));

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var handler = new CreateMemberNonDosenCommandHandler(
                    PenelitianPkmApi.Object,
                    services.GetRequiredService<IMemberNonDosenRepository>(),
                    services.GetRequiredService<IUnitOfWorkNonMember>()
                );

                var command = new CreateMemberNonDosenCommand(PenelitianPkmUuid.ToString(), nomorIdentitas, nama, afiliasi);
                var result = await handler.Handle(command, CancellationToken.None);


                //assert
                Assert.True(result.IsSuccess);
                var memberUuid = result.Value.ToString();

                //var data = DBContextNonDosen.MemberNonDosen.FirstOrDefault(p => p.Uuid.ToString() == memberUuid);
                //Assert.NotNull(data);
                //Assert.Equal(nomorIdentitas, data.NomorIdentitas);
                //Assert.Equal(nama, data.Nama);
            }
        }

        [Fact]
        public async Task Update_ShouldBeExecute_WhenValidData()
        {
            //arrange
            var PenelitianPkmId = "1";
            var PenelitianPkmUuid = Guid.NewGuid();
            var identitasBefore = "987654321";
            var identitasAfter = "123456789";
            var nama = "Mahasiswa Luar";
            var afiliasi = "Universitas X";
            var judul = "uji coba";
            var tahun = "2025-01-01";

            var PenelitianPkmApi = new Mock<IPenelitianPkmApi>();
            PenelitianPkmApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PenelitianPkmResponse(
                    PenelitianPkmId,
                    PenelitianPkmUuid.ToString(),
                    identitasBefore,
                    judul,
                    tahun,
                    null, null, null,
                    "draf",
                    null
                ));

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

            // set Id sesuai PenelitianPkmId
            typeof(Domain.PenelitianPkm.PenelitianPkm).GetProperty(nameof(Domain.PenelitianPkm.PenelitianPkm.Id))!
                .SetValue(hibahEntity, int.Parse(PenelitianPkmId));

            typeof(Domain.PenelitianPkm.PenelitianPkm).GetProperty(nameof(Domain.PenelitianPkm.PenelitianPkm.Uuid))!
                .SetValue(hibahEntity, PenelitianPkmUuid);

            hibahRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(hibahEntity);

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var handler = new CreateMemberNonDosenCommandHandler(
                    PenelitianPkmApi.Object,
                    services.GetRequiredService<IMemberNonDosenRepository>(),
                    services.GetRequiredService<IUnitOfWorkNonMember>()
                );

                var command = new CreateMemberNonDosenCommand(PenelitianPkmUuid.ToString(), identitasBefore, nama, afiliasi);

                Result<Guid> result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result.IsSuccess);
                var memberUuid = result.Value.ToString();

                //var data = DBContextNonDosen.MemberNonDosen.FirstOrDefault(p => p.Uuid.ToString() == memberUuid);
                //Assert.NotNull(data);
                //Assert.Equal(identitasBefore, data.NomorIdentitas);

                //act
                var handlerUpdate = new UpdateMemberNonDosenCommandHandler(
                    services.GetRequiredService<IMemberNonDosenRepository>(),
                    hibahRepository.Object,
                    services.GetRequiredService<IUnitOfWorkNonMember>()
                );

                var commandUpdate = new UpdateMemberNonDosenCommand(memberUuid, PenelitianPkmUuid.ToString(), identitasAfter, nama, afiliasi);
                var resultUpdate = await handlerUpdate.Handle(commandUpdate, default);

                Assert.True(resultUpdate.IsSuccess);

                //var dataUpdate = DBContextNonDosen.MemberNonDosen.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == memberUuid);
                //Assert.NotNull(dataUpdate);
                //Assert.Equal(identitasAfter, dataUpdate.NomorIdentitas);
            }
        }


        [Fact]
        public async Task Delete_ShouldBeExecute_WhenValidData()
        {
            //arrange
            var PenelitianPkmId = "1";
            var PenelitianPkmUuid = Guid.NewGuid();
            var identitas = "123456789";
            var nama = "Mahasiswa Luar";
            var afiliasi = "Universitas Y";
            var judul = "uji coba";
            var tahun = "2025-01-01";

            var PenelitianPkmApi = new Mock<IPenelitianPkmApi>();
            PenelitianPkmApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PenelitianPkmResponse(
                    PenelitianPkmId,
                    PenelitianPkmUuid.ToString(),
                    identitas,
                    judul,
                    tahun,
                    null, null, null,
                    "draf",
                    null
                ));

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var handler = new CreateMemberNonDosenCommandHandler(
                    PenelitianPkmApi.Object,
                    services.GetRequiredService<IMemberNonDosenRepository>(),
                    services.GetRequiredService<IUnitOfWorkNonMember>()
                );

                var command = new CreateMemberNonDosenCommand(PenelitianPkmUuid.ToString(), identitas, nama, afiliasi);

                Result<Guid> result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result.IsSuccess);
                var memberUuid = result.Value.ToString();

                //var data = DBContextNonDosen.MemberNonDosen.FirstOrDefault(p => p.Uuid.ToString() == memberUuid);
                //Assert.NotNull(data);
                //Assert.Equal(identitas, data.NomorIdentitas);

                //act
                var deleteCommand = new DeleteMemberNonDosenCommand(memberUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                //assert
                Assert.True(deleteResult.IsSuccess);
            }
        }

    }
}
