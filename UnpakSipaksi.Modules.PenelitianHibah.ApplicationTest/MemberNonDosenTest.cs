using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Application.CreateMemberNonDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteMemberNonDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateMemberNonDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberNonDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;
using UnpakSipaksi.Modules.PenelitianHibah.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianHibah.ApplicationTest
{
    public class MemberNonDosenTest : BaseIntegrationTest
    {
        public MemberNonDosenTest(IntegrationTestWebAppFactory factory) : base(factory) { }

        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE
            yield return new object[] { empty, empty, "'UuidPenelitianHibah' tidak boleh kosong.", "created" };
            yield return new object[] { empty, "no-guid", "'UuidPenelitianHibah' harus dalam format UUID v4 yang valid.", "created" };

            // UPDATE
            yield return new object[] { empty, valid, "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", valid, "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };
            yield return new object[] { valid, empty, "'UuidPenelitianHibah' tidak boleh kosong.", "updated" };
            yield return new object[] { valid, "no-guid", "'UuidPenelitianHibah' harus dalam format UUID v4 yang valid.", "updated" };

            // DELETE
            yield return new object[] { empty, valid, "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", valid, "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalidFluentValidation(
            string uuid,
            string UuidPenelitianHibah,
            string message,
            string mode)
        {
            Result? result = null;

            if (mode == "created")
            {
                var command = new CreateMemberNonDosenCommand(UuidPenelitianHibah, "", "Nama", "Afiliasi");
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateMemberNonDosenCommand(uuid, UuidPenelitianHibah, "", "Nama Baru", "Afiliasi Baru");
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteMemberNonDosenCommand(uuid);
                result = await Sender.Send(command);
            }

            Assert.True(result.IsFailure);
            if (result.Error is ValidationError validationError)
            {
                Assert.Contains(validationError.Errors, e => e.Description == message);
            }
            else
            {
                Assert.Equal(message, result.Error.Description);
            }
        }

        [Fact]
        public async Task Create_ShouldBeExecute_WhenValidData()
        {
            //arrange
            var penelitianHibahId = "1";
            var penelitianHibahUuid = Guid.NewGuid();
            var nomorIdentitas = "987654321";
            var nama = "Non Dosen Test";
            var afiliasi = "External";
            var judul = "uji coba";
            var tahun = "2025-01-01";

            //act
            var penelitianHibahApi = new Mock<IPenelitianHibahApi>();
            penelitianHibahApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PenelitianHibahResponse(penelitianHibahId, penelitianHibahUuid.ToString(), null, judul, tahun, null, null, null, null, "draf", null));

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var handler = new CreateMemberNonDosenCommandHandler(
                    penelitianHibahApi.Object,
                    services.GetRequiredService<IMemberNonDosenRepository>(),
                    services.GetRequiredService<IUnitOfWorkNonMember>()
                );

                var command = new CreateMemberNonDosenCommand(penelitianHibahUuid.ToString(), nomorIdentitas, nama, afiliasi);
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
            var penelitianHibahId = "1";
            var penelitianHibahUuid = Guid.NewGuid();
            var identitasBefore = "987654321";
            var identitasAfter = "123456789";
            var nama = "Mahasiswa Luar";
            var afiliasi = "Universitas X";
            var judul = "uji coba";
            var tahun = "2025-01-01";

            var penelitianHibahApi = new Mock<IPenelitianHibahApi>();
            penelitianHibahApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PenelitianHibahResponse(
                    penelitianHibahId,
                    penelitianHibahUuid.ToString(),
                    identitasBefore,
                    judul,
                    tahun,
                    null, null, null, null,
                    "draf",
                    null
                ));

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

            // set Id sesuai penelitianHibahId
            typeof(Domain.PenelitianHibah.PenelitianHibah).GetProperty(nameof(Domain.PenelitianHibah.PenelitianHibah.Id))!
                .SetValue(hibahEntity, int.Parse(penelitianHibahId));

            typeof(Domain.PenelitianHibah.PenelitianHibah).GetProperty(nameof(Domain.PenelitianHibah.PenelitianHibah.Uuid))!
                .SetValue(hibahEntity, penelitianHibahUuid);

            hibahRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(hibahEntity);

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var handler = new CreateMemberNonDosenCommandHandler(
                    penelitianHibahApi.Object,
                    services.GetRequiredService<IMemberNonDosenRepository>(),
                    services.GetRequiredService<IUnitOfWorkNonMember>()
                );

                var command = new CreateMemberNonDosenCommand(penelitianHibahUuid.ToString(), identitasBefore, nama, afiliasi);

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

                var commandUpdate = new UpdateMemberNonDosenCommand(memberUuid, penelitianHibahUuid.ToString(), identitasAfter, nama, afiliasi);
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
            var penelitianHibahId = "1";
            var penelitianHibahUuid = Guid.NewGuid();
            var identitas = "123456789";
            var nama = "Mahasiswa Luar";
            var afiliasi = "Universitas Y";
            var judul = "uji coba";
            var tahun = "2025-01-01";

            var penelitianHibahApi = new Mock<IPenelitianHibahApi>();
            penelitianHibahApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PenelitianHibahResponse(
                    penelitianHibahId,
                    penelitianHibahUuid.ToString(),
                    identitas,
                    judul,
                    tahun,
                    null, null, null, null,
                    "draf",
                    null
                ));

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var handler = new CreateMemberNonDosenCommandHandler(
                    penelitianHibahApi.Object,
                    services.GetRequiredService<IMemberNonDosenRepository>(),
                    services.GetRequiredService<IUnitOfWorkNonMember>()
                );

                var command = new CreateMemberNonDosenCommand(penelitianHibahUuid.ToString(), identitas, nama, afiliasi);

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
