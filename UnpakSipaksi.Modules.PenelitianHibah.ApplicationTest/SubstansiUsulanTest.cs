using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Application.CreateSubstansiUsulan;
using UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteSubstansiUsulan;
using UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateSubstansiUsulan;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.Substansi;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianHibah.ApplicationTest
{
    public class SubstansiUsulanTest : BaseIntegrationTest
    {
        public SubstansiUsulanTest(IntegrationTestWebAppFactory factory) : base(factory) { }

        //public static IEnumerable<object[]> InvalidData()
        //{
        //    var validUuid = Guid.NewGuid().ToString();
        //    var empty = "";
        //    var invalidGuid = "invalid-guid";

        //    // UuidPenelitianHibah
        //    yield return new object[] { empty, "file.pdf", "'UuidPenelitianHibah' tidak boleh kosong." };
        //    yield return new object[] { invalidGuid, "file.pdf", "'UuidPenelitianHibah' harus dalam format UUID v4 yang valid." };

        //    // File
        //    yield return new object[] { validUuid, "", "'File' tidak boleh kosong." };
        //}

        //[Theory]
        //[MemberData(nameof(InvalidData))]
        //public async Task CreateSubstansiUsulan_ShouldFailValidation_WhenInvalid(
        //    string uuidPenelitianHibah,
        //    string? file,
        //    string expectedMessage)
        //{
        //    var command = new CreateSubstansiUsulanCommand(
        //        uuidPenelitianHibah,
        //        file
        //    );

        //    var validator = new CreateSubstansiUsulanCommandValidator();
        //    var result = await validator.ValidateAsync(command);

        //    Assert.False(result.IsValid);
        //    Assert.Contains(result.Errors, e => e.ErrorMessage == expectedMessage);
        //}

        [Fact]
        public async Task CreateSubstansiUsulan_ShouldBeSuccess_WhenValidData()
        {
            var penelitianHibahId = "1";
            var penelitianHibahUuid = Guid.NewGuid();
            var NIDNBefore = "1234567890";

            var unitOfWork = new Mock<IUnitOfWorkSubstansi>();

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

                var handler = new CreateSubstansiUsulanCommandHandler(
                    services.GetRequiredService<ISubstansiRepository>(),
                    hibahRepository.Object,
                    unitOfWork.Object
                );

                var command = new CreateSubstansiUsulanCommand(
                    penelitianHibahUuid.ToString(),
                    "file.pdf"
                );

                var result = await handler.Handle(command, CancellationToken.None);
                var dokumenUuid = result.Value.ToString();

                Assert.True(result.IsSuccess);
                //var data = DBContextSubstansiUsulan.Substansi.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == dokumenUuid);
                //Assert.NotNull(data);
                //Assert.Equal("file.pdf", data.File);
            }
        }

        [Fact]
        public async Task UpdateSubstansiUsulan_ShouldBeExecute_WhenValidData()
        {
            var penelitianHibahId = "1";
            var penelitianHibahUuid = Guid.NewGuid();
            var NIDNBefore = "1234567890";

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
                var unitOfWork = services.GetRequiredService<IUnitOfWorkSubstansi>();

                var handler = new CreateSubstansiUsulanCommandHandler(
                    services.GetRequiredService<ISubstansiRepository>(),
                    hibahRepository.Object,
                    unitOfWork
                );

                var command = new CreateSubstansiUsulanCommand(
                    penelitianHibahUuid.ToString(),
                    "file.pdf"
                );

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result.IsSuccess);
                var substansiUuid = result.Value.ToString();
                //var data = DBContextSubstansiUsulan.Substansi.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == substansiUuid);
                //Assert.NotNull(data);
                //Assert.Equal("file.pdf", data.File);

                var substansiRepository = new Mock<ISubstansiRepository>();

                var substansiEntity = Domain.Substansi.Substansi
                    .Create(
                        penelitianHibahUuid,
                        hibahEntity,
                        "file.pdf"
                    ).Value;

                typeof(Domain.Substansi.Substansi).GetProperty(nameof(Domain.Substansi.Substansi.Id))!
                    .SetValue(substansiEntity, 123);

                typeof(Domain.Substansi.Substansi).GetProperty(nameof(Domain.Substansi.Substansi.Uuid))!
                    .SetValue(substansiEntity, Guid.Parse(substansiUuid));

                substansiRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(substansiEntity);

                var updateHandler = new UpdateSubstansiUsulanCommandHandler(
                    substansiRepository.Object,
                    hibahRepository.Object,
                    unitOfWork
                );

                var updateCommand = new UpdateSubstansiUsulanCommand(
                    substansiUuid,
                    penelitianHibahUuid.ToString(),
                    "file2.pdf"
                );

                var updateResult = await updateHandler.Handle(updateCommand, CancellationToken.None);

                // Assert
                Assert.True(updateResult.IsSuccess);
                //var dataUpdate = DBContextSubstansiUsulan.Substansi.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == substansiUuid);
                //Assert.NotNull(dataUpdate);
                //Assert.Equal("file2.pdf", dataUpdate.File);
            }
        }

        [Fact]
        public async Task DeleteSubstansiUsulan_ShouldBeExecute_WhenValidData()
        {
            var penelitianHibahId = "1";
            var penelitianHibahUuid = Guid.NewGuid();
            var NIDNBefore = "1234567890";

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
                var unitOfWork = services.GetRequiredService<IUnitOfWorkSubstansi>();

                var handler = new CreateSubstansiUsulanCommandHandler(
                    services.GetRequiredService<ISubstansiRepository>(),
                    hibahRepository.Object,
                    unitOfWork
                );

                var command = new CreateSubstansiUsulanCommand(
                    penelitianHibahUuid.ToString(),
                    "file.pdf"
                );

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.True(result.IsSuccess);
                var substansiUuid = result.Value.ToString();
                //var data = DBContextSubstansiUsulan.Substansi.AsNoTracking().FirstOrDefault(p => p.Uuid.ToString() == substansiUuid);
                //Assert.NotNull(data);
                //Assert.Equal("file.pdf", data.File);

                var substansiRepository = new Mock<ISubstansiRepository>();

                var substansiEntity = Domain.Substansi.Substansi
                    .Create(
                        penelitianHibahUuid,
                        hibahEntity,
                        "file.pdf"
                    ).Value;

                typeof(Domain.Substansi.Substansi).GetProperty(nameof(Domain.Substansi.Substansi.Id))!
                    .SetValue(substansiEntity, 123);

                typeof(Domain.Substansi.Substansi).GetProperty(nameof(Domain.Substansi.Substansi.Uuid))!
                    .SetValue(substansiEntity, Guid.Parse(substansiUuid));

                substansiRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(substansiEntity);

                var deleteHandler = new DeleteSubstansiUsulanCommandHandler(
                     substansiRepository.Object,
                     hibahRepository.Object,
                     unitOfWork
                 );

                var deleteCommand = new DeleteSubstansiUsulanCommand(substansiUuid, penelitianHibahUuid.ToString());
                var deleteResult = await deleteHandler.Handle(deleteCommand, CancellationToken.None);

                Assert.True(deleteResult.IsSuccess);
            }
        }


    }
}
