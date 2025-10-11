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
    public class MemberDosenTestPart3 : BaseIntegrationTest
    {
        public MemberDosenTestPart3(IntegrationTestWebAppFactory factory) : base(factory) { }

        [Fact]
        public async Task Update_ShouldBeExecute_WhenValidData()
        {
            //arrange
            var penelitianHibahId = "1";
            var penelitianHibahUuid = Guid.NewGuid();
            var NIDNBefore = "1234567890";
            var NIDNAfter = "1234567891";
            var judul = "uji coba";
            var tahun = "2025-01-01";

            var penelitianHibahApi = new Mock<IPenelitianHibahApi>();
            penelitianHibahApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PenelitianHibahResponse(penelitianHibahId, penelitianHibahUuid.ToString(), NIDNBefore, judul, tahun, null, null, null, null, "draf", null));

            var hibahRepository = new Mock<IPenelitianHibahRepository>();
            hibahRepository.Setup(r => r.HasUniqueDataAsync(
                    It.IsAny<Guid?>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var hibahEntity = Domain.PenelitianHibah.PenelitianHibah
                .Create(hibahRepository.Object, NIDNBefore, "2025-01-01", "Judul")
                .Result.Value;

            typeof(Domain.PenelitianHibah.PenelitianHibah).GetProperty(nameof(Domain.PenelitianHibah.PenelitianHibah.Id))!
                .SetValue(hibahEntity, int.Parse(penelitianHibahId));

            typeof(Domain.PenelitianHibah.PenelitianHibah).GetProperty(nameof(Domain.PenelitianHibah.PenelitianHibah.Uuid))!
                .SetValue(hibahEntity, penelitianHibahUuid);

            hibahRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(hibahEntity); 

            using (var scope = Factory.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var handler = new CreateMemberDosenCommandHandler(
                    penelitianHibahApi.Object,
                    services.GetRequiredService<IMemberDosenRepository>(),
                    services.GetRequiredService<IUnitOfWorkMember>()
                );

                var command = new CreateMemberDosenCommand(penelitianHibahUuid.ToString(), NIDNBefore);

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

                var commandUpdate = new UpdateMemberDosenCommand(memberUuid, penelitianHibahUuid.ToString(), NIDNAfter);
                var resultUpdate = await handlerUpdate.Handle(commandUpdate, default);

                Assert.True(resultUpdate.IsSuccess);

                //var dataUpdate = DBContextDosen.MemberDosen.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == memberUuid);
                //Assert.NotNull(dataUpdate);
                //Assert.Equal(NIDNAfter, dataUpdate.NIDN);
            }
        }

    }
}
