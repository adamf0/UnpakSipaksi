using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Application.CreateMemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteMemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateMemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;
using UnpakSipaksi.Modules.PenelitianHibah.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianHibah.ApplicationTest
{
    public class MemberMahasiswaTestPart2 : BaseIntegrationTest
    {
        public MemberMahasiswaTestPart2(IntegrationTestWebAppFactory factory) : base(factory) { }


        [Fact]
        public async Task Delete_ShouldBeExecute_WhenValidData()
        {
            //arrange
            var penelitianHibahId = "1";
            var penelitianHibahUuid = Guid.NewGuid();
            var NPM = "123456789";
            var judul = "uji coba";
            var tahun = "2025-01-01";

            var penelitianHibahApi = new Mock<IPenelitianHibahApi>();

            penelitianHibahApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PenelitianHibahResponse(penelitianHibahId, penelitianHibahUuid.ToString(), NPM, judul, tahun, null, null, null, null, "draf", null));

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var handler = new CreateMemberMahasiswaCommandHandler(
                    penelitianHibahApi.Object,
                    services.GetRequiredService<IMemberMahasiswaRepository>(),
                    services.GetRequiredService<IUnitOfWorkMemberMahasiswa>()
                );

                var command = new CreateMemberMahasiswaCommand(penelitianHibahUuid.ToString(), NPM);

                Result<Guid> result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result.IsSuccess);
                var memberUuid = result.Value.ToString();

                //var data = DBContextMahasiswa.MemberMahasiswa.FirstOrDefault(p => p.Uuid.ToString() == memberUuid);
                //Assert.NotNull(data);
                //Assert.Equal(NPM, data.NPM);


                //act
                var deleteCommand = new DeleteMemberMahasiswaCommand(memberUuid, NPM);
                var deleteResult = await Sender.Send(deleteCommand);

                //assert
                Assert.True(deleteResult.IsSuccess);
            }
        }
    }
}
