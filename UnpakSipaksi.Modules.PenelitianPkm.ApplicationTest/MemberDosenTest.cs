using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianPkm.Application.CreateMemberDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Application.DeleteMemberDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateMemberDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianPkm.ApplicationTest
{
    public class MemberDosenTest : BaseIntegrationTest
    {
        public MemberDosenTest(IntegrationTestWebAppFactory factory) : base(factory) { }

        //public static IEnumerable<object[]> InvalidData()
        //{
        //    var valid = Guid.NewGuid().ToString();
        //    var empty = "";

        //    // CREATE
        //    yield return new object[] { empty, "", "'NIDN' tidak boleh kosong.", "created" };
        //    yield return new object[] { empty, "abc", "'UuidPenelitianPkm' tidak boleh kosong.", "created" };
        //    yield return new object[] { "no-guid", "1234567890", "'UuidPenelitianPkm' harus dalam format UUID v4 yang valid.", "created" };
        //    yield return new object[] { valid, "abc", "'NIDN' tidak valid.", "created" };

        //    // UPDATE
        //    yield return new object[] { empty, "abc", "'UuidPenelitianPkm' tidak boleh kosong.", "updated" };
        //    yield return new object[] { "no-guid", "1234567890", "'UuidPenelitianPkm' harus dalam format UUID v4 yang valid.", "updated" };
        //    yield return new object[] { valid, "", "'NIDN' tidak boleh kosong.", "updated" };
        //    yield return new object[] { valid, "abc", "'NIDN' tidak valid.", "updated" };

        //    // DELETE
        //    yield return new object[] { empty, "1234567890", "'Uuid' tidak boleh kosong.", "deleted" };
        //    yield return new object[] { "no-guid", "1234567890", "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        //}


        //[Theory]
        //[MemberData(nameof(InvalidData))]
        //public async Task CreateUpdateDelete_ShouldThrow_WhenInvalidFluentValidation(
        //    string uuid,
        //    string nidn,
        //    string message,
        //    string mode)
        //{
        //    Result? result = null;

        //    if (mode == "created")
        //    {
        //        var command = new CreateMemberDosenCommand(uuid, nidn);
        //        result = await Sender.Send(command);
        //    }
        //    else if (mode == "updated")
        //    {
        //        var command = new UpdateMemberDosenCommand(uuid, uuid, nidn);
        //        result = await Sender.Send(command);
        //    }
        //    else
        //    {
        //        var command = new DeleteMemberDosenCommand(uuid, nidn);
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
            var NIDN = "1234567890";
            var judul = "uji coba";
            var tahun = "2025-01-01";

            //act
            var PenelitianPkmApi = new Mock<IPenelitianPkmApi>();
            
            PenelitianPkmApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PenelitianPkmResponse(PenelitianPkmId, PenelitianPkmUuid.ToString(), NIDN, judul, tahun, null, null, null, "draf", null));

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var handler = new CreateMemberDosenCommandHandler(
                    PenelitianPkmApi.Object,
                    services.GetRequiredService<IMemberDosenRepository>(),
                    services.GetRequiredService<IUnitOfWorkMember>()
                );

                var command = new CreateMemberDosenCommand(PenelitianPkmUuid.ToString(), NIDN);

                Result<Guid> result = await handler.Handle(command, CancellationToken.None);

                //assert
                Assert.True(result.IsSuccess);
                var memberUuid = result.Value.ToString();

                //var data = DBContextDosen.MemberDosen.FirstOrDefault(p => p.Uuid.ToString() == memberUuid);
                //Assert.NotNull(data);
                //Assert.Equal(NIDN, data.NIDN);
            }
        }

        [Fact]
        public async Task Update_ShouldBeExecute_WhenValidData()
        {
            //arrange
            var PenelitianPkmId = "1";
            var PenelitianPkmUuid = Guid.NewGuid();
            var NIDNBefore = "1234567890";
            var NIDNAfter = "1234567891";
            var judul = "uji coba";
            var tahun = "2025-01-01";

            var PenelitianPkmApi = new Mock<IPenelitianPkmApi>();
            PenelitianPkmApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PenelitianPkmResponse(PenelitianPkmId, PenelitianPkmUuid.ToString(), NIDNBefore, judul, tahun, null, null, null, "draf", null));

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
                var handler = new CreateMemberDosenCommandHandler(
                    PenelitianPkmApi.Object,
                    services.GetRequiredService<IMemberDosenRepository>(),
                    services.GetRequiredService<IUnitOfWorkMember>()
                );

                var command = new CreateMemberDosenCommand(PenelitianPkmUuid.ToString(), NIDNBefore);

                Result<Guid> result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result.IsSuccess);
                var memberUuid = result.Value.ToString();

                //var data = DBContextDosen.MemberDosen.FirstOrDefault(p => p.Uuid.ToString() == memberUuid);
                //Assert.NotNull(data);
                //Assert.Equal(NIDNBefore, data.NIDN);

                //act
                var handlerUpdate = new UpdateMemberDosenCommandHandler(
                    services.GetRequiredService<IMemberDosenRepository>(),
                    hibahRepository.Object,
                    services.GetRequiredService<IUnitOfWorkMember>()
                );

                var commandUpdate = new UpdateMemberDosenCommand(memberUuid, PenelitianPkmUuid.ToString(), NIDNAfter);
                var resultUpdate = await handlerUpdate.Handle(commandUpdate, default);

                Assert.True(resultUpdate.IsSuccess);

                //var dataUpdate = DBContextDosen.MemberDosen.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == memberUuid);
                //Assert.NotNull(dataUpdate);
                //Assert.Equal(NIDNAfter, dataUpdate.NIDN);
            }
        }

        [Fact]
        public async Task Delete_ShouldBeExecute_WhenValidData()
        {
            //arrange
            var PenelitianPkmId = "1";
            var PenelitianPkmUuid = Guid.NewGuid();
            var NIDN = "1234567890";
            var judul = "uji coba";
            var tahun = "2025-01-01";

            var PenelitianPkmApi = new Mock<IPenelitianPkmApi>();

            PenelitianPkmApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PenelitianPkmResponse(PenelitianPkmId, PenelitianPkmUuid.ToString(), NIDN, judul, tahun, null, null, null, "draf", null));

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var handler = new CreateMemberDosenCommandHandler(
                    PenelitianPkmApi.Object,
                    services.GetRequiredService<IMemberDosenRepository>(),
                    services.GetRequiredService<IUnitOfWorkMember>()
                );

                var command = new CreateMemberDosenCommand(PenelitianPkmUuid.ToString(), NIDN);

                Result<Guid> result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result.IsSuccess);
                var memberUuid = result.Value.ToString();

                //var data = DBContextDosen.MemberDosen.FirstOrDefault(p => p.Uuid.ToString() == memberUuid);
                //Assert.NotNull(data);
                //Assert.Equal(NIDN, data.NIDN);


                //act
                var deleteCommand = new DeleteMemberDosenCommand(memberUuid, NIDN);
                var deleteResult = await Sender.Send(deleteCommand);

                //assert
                Assert.True(deleteResult.IsSuccess);
            }
        }
    }
}
