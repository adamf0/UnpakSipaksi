using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianPkm.Application.CreateMemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianPkm.Application.DeleteMemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateMemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianPkm.ApplicationTest
{
    public class MemberMahasiswaTest : BaseIntegrationTest
    {
        public MemberMahasiswaTest(IntegrationTestWebAppFactory factory) : base(factory) { }

        //public static IEnumerable<object[]> InvalidData()
        //{
        //    var valid = Guid.NewGuid().ToString();
        //    var empty = "";

        //    // CREATE
        //    yield return new object[] { empty, "", "'NPM' tidak boleh kosong.", "created" };
        //    yield return new object[] { empty, "abc", "'UuidPenelitianPkm' tidak boleh kosong.", "created" };
        //    yield return new object[] { "no-guid", "123456789", "'UuidPenelitianPkm' harus dalam format UUID v4 yang valid.", "created" };
        //    yield return new object[] { valid, "abc", "'NPM' tidak valid.", "created" };

        //    // UPDATE
        //    yield return new object[] { empty, "abc", "'UuidPenelitianPkm' tidak boleh kosong.", "updated" };
        //    yield return new object[] { "no-guid", "123456789", "'UuidPenelitianPkm' harus dalam format UUID v4 yang valid.", "updated" };
        //    yield return new object[] { valid, "", "'NPM' tidak boleh kosong.", "updated" };
        //    yield return new object[] { valid, "abc", "'NPM' tidak valid.", "updated" };

        //    // DELETE
        //    yield return new object[] { empty, "123456789", "'Uuid' tidak boleh kosong.", "deleted" };
        //    yield return new object[] { "no-guid", "123456789", "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        //}


        //[Theory]
        //[MemberData(nameof(InvalidData))]
        //public async Task CreateUpdateDelete_ShouldThrow_WhenInvalidFluentValidation(
        //    string uuid,
        //    string NPM,
        //    string message,
        //    string mode)
        //{
        //    Result? result = null;

        //    if (mode == "created")
        //    {
        //        var command = new CreateMemberMahasiswaCommand(uuid, NPM);
        //        result = await Sender.Send(command);
        //    }
        //    else if (mode == "updated")
        //    {
        //        var command = new UpdateMemberMahasiswaCommand(uuid, uuid, NPM);
        //        result = await Sender.Send(command);
        //    }
        //    else
        //    {
        //        var command = new DeleteMemberMahasiswaCommand(uuid, NPM);
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
            var NPM = "123456789";
            var judul = "uji coba";
            var tahun = "2025-01-01";

            //act
            var PenelitianPkmApi = new Mock<IPenelitianPkmApi>();
            
            PenelitianPkmApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PenelitianPkmResponse(PenelitianPkmId, PenelitianPkmUuid.ToString(), NPM, judul, tahun, null, null, null, "draf", null));

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var handler = new CreateMemberMahasiswaCommandHandler(
                    PenelitianPkmApi.Object,
                    services.GetRequiredService<IMemberMahasiswaRepository>(),
                    services.GetRequiredService<IUnitOfWorkMemberMahasiswa>()
                );

                var command = new CreateMemberMahasiswaCommand(PenelitianPkmUuid.ToString(), NPM);

                Result<Guid> result = await handler.Handle(command, CancellationToken.None);

                //assert
                Assert.True(result.IsSuccess);
                var memberUuid = result.Value.ToString();

                //var data = DBContextMahasiswa.MemberMahasiswa.FirstOrDefault(p => p.Uuid.ToString() == memberUuid);
                //Assert.NotNull(data);
                //Assert.Equal(NPM, data.NPM);
            }
        }

        //[Fact]
        //public async Task Update_ShouldBeExecute_WhenValidData()
        //{
        //    //arrange
        //    var PenelitianPkmId = "1";
        //    var PenelitianPkmUuid = Guid.NewGuid();
        //    var NPMBefore = "123456789";
        //    var NPMAfter = "123456788";
        //    var NIDNBefore = "1234567890";
        //    var judul = "uji coba";
        //    var tahun = "2025-01-01";

        //    var PenelitianPkmApi = new Mock<IPenelitianPkmApi>();
        //    PenelitianPkmApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(new PenelitianPkmResponse(PenelitianPkmId, PenelitianPkmUuid.ToString(), NPMBefore, judul, tahun, null, null, null, "draf", null));

        //    var hibahRepository = new Mock<IPenelitianPkmRepository>();
        //    hibahRepository.Setup(r => r.HasUniqueDataAsync(
        //            It.IsAny<Guid?>(),
        //            It.IsAny<string>(),
        //            It.IsAny<string>(),
        //            It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(true);

        //    var hibahEntity = Domain.PenelitianPkm.PenelitianPkm
        //        .Create(hibahRepository.Object, NIDNBefore, "2025-01-01", "Judul")
        //        .Result.Value;

        //    typeof(Domain.PenelitianPkm.PenelitianPkm).GetProperty(nameof(Domain.PenelitianPkm.PenelitianPkm.Id))!
        //        .SetValue(hibahEntity, int.Parse(PenelitianPkmId));

        //    typeof(Domain.PenelitianPkm.PenelitianPkm).GetProperty(nameof(Domain.PenelitianPkm.PenelitianPkm.Uuid))!
        //        .SetValue(hibahEntity, PenelitianPkmUuid);

        //    hibahRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
        //                       .ReturnsAsync(hibahEntity); 

        //    using (var scope = Factory.Services.CreateScope())
        //    {
        //        var services = scope.ServiceProvider;
        //        var handler = new CreateMemberMahasiswaCommandHandler(
        //            PenelitianPkmApi.Object,
        //            services.GetRequiredService<IMemberMahasiswaRepository>(),
        //            services.GetRequiredService<IUnitOfWorkMemberMahasiswa>()
        //        );

        //        var command = new CreateMemberMahasiswaCommand(PenelitianPkmUuid.ToString(), NPMBefore);

        //        Result<Guid> result = await handler.Handle(command, CancellationToken.None);

        //        Assert.True(result.IsSuccess);
        //        var memberUuid = result.Value.ToString();

        //        //var data = DBContextMahasiswa.MemberMahasiswa.FirstOrDefault(p => p.Uuid.ToString() == memberUuid);
        //        //Assert.NotNull(data);
        //        //Assert.Equal(NPMBefore, data.NPM);

        //        //act
        //        var handlerUpdate = new UpdateMemberMahasiswaCommandHandler(
        //            services.GetRequiredService<IMemberMahasiswaRepository>(),
        //            hibahRepository.Object,
        //            services.GetRequiredService<IUnitOfWorkMemberMahasiswa>()
        //        );

        //        var commandUpdate = new UpdateMemberMahasiswaCommand(memberUuid, PenelitianPkmUuid.ToString(), NPMAfter);
        //        var resultUpdate = await handlerUpdate.Handle(commandUpdate, default);

        //        Assert.True(resultUpdate.IsSuccess);

        //        //var dataUpdate = DBContextMahasiswa.MemberMahasiswa.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == memberUuid);
        //        //Assert.NotNull(dataUpdate);
        //        //Assert.Equal(NPMAfter, dataUpdate.NPM);
        //    }
        //}

        //[Fact]
        //public async Task Delete_ShouldBeExecute_WhenValidData()
        //{
        //    //arrange
        //    var PenelitianPkmId = "1";
        //    var PenelitianPkmUuid = Guid.NewGuid();
        //    var NPM = "123456789";
        //    var judul = "uji coba";
        //    var tahun = "2025-01-01";

        //    var PenelitianPkmApi = new Mock<IPenelitianPkmApi>();

        //    PenelitianPkmApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(new PenelitianPkmResponse(PenelitianPkmId, PenelitianPkmUuid.ToString(), NPM, judul, tahun, null, null, null, "draf", null));

        //    using (var scope = Factory.Services.CreateScope())
        //    {
        //        var services = scope.ServiceProvider;
        //        var handler = new CreateMemberMahasiswaCommandHandler(
        //            PenelitianPkmApi.Object,
        //            services.GetRequiredService<IMemberMahasiswaRepository>(),
        //            services.GetRequiredService<IUnitOfWorkMemberMahasiswa>()
        //        );

        //        var command = new CreateMemberMahasiswaCommand(PenelitianPkmUuid.ToString(), NPM);

        //        Result<Guid> result = await handler.Handle(command, CancellationToken.None);

        //        Assert.True(result.IsSuccess);
        //        var memberUuid = result.Value.ToString();

        //        //var data = DBContextMahasiswa.MemberMahasiswa.FirstOrDefault(p => p.Uuid.ToString() == memberUuid);
        //        //Assert.NotNull(data);
        //        //Assert.Equal(NPM, data.NPM);


        //        //act
        //        var deleteCommand = new DeleteMemberMahasiswaCommand(memberUuid, NPM);
        //        var deleteResult = await Sender.Send(deleteCommand);

        //        //assert
        //        Assert.True(deleteResult.IsSuccess);
        //    }
        //}
    }
}
