using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Application.CreateMemberDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteMemberDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateMemberDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;
using UnpakSipaksi.Modules.PenelitianHibah.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianHibah.ApplicationTest
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
        //    yield return new object[] { empty, "abc", "'UuidPenelitianHibah' tidak boleh kosong.", "created" };
        //    yield return new object[] { "no-guid", "1234567890", "'UuidPenelitianHibah' harus dalam format UUID v4 yang valid.", "created" };
        //    yield return new object[] { valid, "abc", "'NIDN' tidak valid.", "created" };

        //    // UPDATE
        //    yield return new object[] { empty, "abc", "'UuidPenelitianHibah' tidak boleh kosong.", "updated" };
        //    yield return new object[] { "no-guid", "1234567890", "'UuidPenelitianHibah' harus dalam format UUID v4 yang valid.", "updated" };
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
            var penelitianHibahId = "1";
            var penelitianHibahUuid = Guid.NewGuid();
            var NIDN = "1234567890";
            var judul = "uji coba";
            var tahun = "2025-01-01";

            //act
            var penelitianHibahApi = new Mock<IPenelitianHibahApi>();
            
            penelitianHibahApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PenelitianHibahResponse(penelitianHibahId, penelitianHibahUuid.ToString(), NIDN, judul, tahun, null, null, null, null, "draf", null));

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var handler = new CreateMemberDosenCommandHandler(
                    penelitianHibahApi.Object,
                    services.GetRequiredService<IMemberDosenRepository>(),
                    services.GetRequiredService<IUnitOfWorkMember>()
                );

                var command = new CreateMemberDosenCommand(penelitianHibahUuid.ToString(), NIDN);

                Result<Guid> result = await handler.Handle(command, CancellationToken.None);

                //assert
                Assert.True(result.IsSuccess);
                var memberUuid = result.Value.ToString();

                //var data = DBContextDosen.MemberDosen.FirstOrDefault(p => p.Uuid.ToString() == memberUuid);
                //Assert.NotNull(data);
                //Assert.Equal(NIDN, data.NIDN);
            }
        }
    }
}
