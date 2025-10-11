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
    public class MemberDosenTestPart2 : BaseIntegrationTest
    {
        public MemberDosenTestPart2(IntegrationTestWebAppFactory factory) : base(factory) { }


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
