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
    public class MemberMahasiswaTestPart2 : BaseIntegrationTest
    {
        public MemberMahasiswaTestPart2(IntegrationTestWebAppFactory factory) : base(factory) { }

        [Fact]
        public async Task Delete_ShouldBeExecute_WhenValidData()
        {
            //arrange
            var PenelitianPkmId = "1";
            var PenelitianPkmUuid = Guid.NewGuid();
            var NPM = "123456789";
            var judul = "uji coba";
            var tahun = "2025-01-01";

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
