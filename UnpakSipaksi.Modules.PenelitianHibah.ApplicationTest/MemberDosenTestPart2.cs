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
    public class MemberDosenTestPart2 : BaseIntegrationTest
    {
        public MemberDosenTestPart2(IntegrationTestWebAppFactory factory) : base(factory) { }

        [Fact]
        public async Task Delete_ShouldBeExecute_WhenValidData()
        {
            //arrange
            var penelitianHibahId = "1";
            var penelitianHibahUuid = Guid.NewGuid();
            var NIDN = "1234567890";
            var judul = "uji coba";
            var tahun = "2025-01-01";

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
