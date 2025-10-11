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
    public class MemberDosenTestPart3 : BaseIntegrationTest
    {
        public MemberDosenTestPart3(IntegrationTestWebAppFactory factory) : base(factory) { }


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
    }
}
