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
    public class MemberMahasiswaTestPart3 : BaseIntegrationTest
    {
        public MemberMahasiswaTestPart3(IntegrationTestWebAppFactory factory) : base(factory) { }

        [Fact]
        public async Task Update_ShouldBeExecute_WhenValidData()
        {
            //arrange
            var penelitianHibahId = "1";
            var penelitianHibahUuid = Guid.NewGuid();
            var NPMBefore = "123456789";
            var NPMAfter = "123456788";
            var NIDNBefore = "1234567890";
            var judul = "uji coba";
            var tahun = "2025-01-01";

            var penelitianHibahApi = new Mock<IPenelitianHibahApi>();
            penelitianHibahApi.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PenelitianHibahResponse(penelitianHibahId, penelitianHibahUuid.ToString(), NPMBefore, judul, tahun, null, null, null, null, "draf", null));

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
                var handler = new CreateMemberMahasiswaCommandHandler(
                    penelitianHibahApi.Object,
                    services.GetRequiredService<IMemberMahasiswaRepository>(),
                    services.GetRequiredService<IUnitOfWorkMemberMahasiswa>()
                );

                var command = new CreateMemberMahasiswaCommand(penelitianHibahUuid.ToString(), NPMBefore);

                Result<Guid> result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result.IsSuccess);
                var memberUuid = result.Value.ToString();

                //var data = DBContextMahasiswa.MemberMahasiswa.FirstOrDefault(p => p.Uuid.ToString() == memberUuid);
                //Assert.NotNull(data);
                //Assert.Equal(NPMBefore, data.NPM);

                //act
                var handlerUpdate = new UpdateMemberMahasiswaCommandHandler(
                    services.GetRequiredService<IMemberMahasiswaRepository>(),
                    hibahRepository.Object,
                    services.GetRequiredService<IUnitOfWorkMemberMahasiswa>()
                );

                var commandUpdate = new UpdateMemberMahasiswaCommand(memberUuid, penelitianHibahUuid.ToString(), NPMAfter);
                var resultUpdate = await handlerUpdate.Handle(commandUpdate, default);

                Assert.True(resultUpdate.IsSuccess);

                //var dataUpdate = DBContextMahasiswa.MemberMahasiswa.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == memberUuid);
                //Assert.NotNull(dataUpdate);
                //Assert.Equal(NPMAfter, dataUpdate.NPM);
            }
        }
    }
}
