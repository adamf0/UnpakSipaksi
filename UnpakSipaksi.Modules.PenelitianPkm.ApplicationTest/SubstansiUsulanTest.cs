using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianPkm.Application.CreateSubstansiUsulan;
using UnpakSipaksi.Modules.PenelitianPkm.Application.DeleteSubstansiUsulan;
using UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateSubstansiUsulan;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.Substansi;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianPkm.ApplicationTest
{
    public class SubstansiUsulanTest : BaseIntegrationTest
    {
        public SubstansiUsulanTest(IntegrationTestWebAppFactory factory) : base(factory) { }

        //public static IEnumerable<object[]> InvalidData()
        //{
        //    var validUuid = Guid.NewGuid().ToString();
        //    var empty = "";
        //    var invalidGuid = "invalid-guid";

        //    // UuidPenelitianPkm
        //    yield return new object[] { empty, "file.pdf", "'UuidPenelitianPkm' tidak boleh kosong." };
        //    yield return new object[] { invalidGuid, "file.pdf", "'UuidPenelitianPkm' harus dalam format UUID v4 yang valid." };

        //    // File
        //    yield return new object[] { validUuid, "", "'File' tidak boleh kosong." };
        //}

        //[Theory]
        //[MemberData(nameof(InvalidData))]
        //public async Task CreateSubstansiUsulan_ShouldFailValidation_WhenInvalid(
        //    string uuidPenelitianPkm,
        //    string? file,
        //    string expectedMessage)
        //{
        //    var command = new CreateSubstansiUsulanCommand(
        //        uuidPenelitianPkm,
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
            var PenelitianPkmId = "1";
            var PenelitianPkmUuid = Guid.NewGuid();
            var NIDNBefore = "1234567890";

            var unitOfWork = new Mock<IUnitOfWorkSubstansi>();

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

                var handler = new CreateSubstansiUsulanCommandHandler(
                    services.GetRequiredService<ISubstansiRepository>(),
                    hibahRepository.Object,
                    unitOfWork.Object
                );

                var command = new CreateSubstansiUsulanCommand(
                    PenelitianPkmUuid.ToString(),
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
            var PenelitianPkmId = "1";
            var PenelitianPkmUuid = Guid.NewGuid();
            var NIDNBefore = "1234567890";

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
                var unitOfWork = services.GetRequiredService<IUnitOfWorkSubstansi>();

                var handler = new CreateSubstansiUsulanCommandHandler(
                    services.GetRequiredService<ISubstansiRepository>(),
                    hibahRepository.Object,
                    unitOfWork
                );

                var command = new CreateSubstansiUsulanCommand(
                    PenelitianPkmUuid.ToString(),
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
                        PenelitianPkmUuid,
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
                    PenelitianPkmUuid.ToString(),
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
            var PenelitianPkmId = "1";
            var PenelitianPkmUuid = Guid.NewGuid();
            var NIDNBefore = "1234567890";

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
                var unitOfWork = services.GetRequiredService<IUnitOfWorkSubstansi>();

                var handler = new CreateSubstansiUsulanCommandHandler(
                    services.GetRequiredService<ISubstansiRepository>(),
                    hibahRepository.Object,
                    unitOfWork
                );

                var command = new CreateSubstansiUsulanCommand(
                    PenelitianPkmUuid.ToString(),
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
                        PenelitianPkmUuid,
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

                var deleteCommand = new DeleteSubstansiUsulanCommand(substansiUuid, PenelitianPkmUuid.ToString());
                var deleteResult = await deleteHandler.Handle(deleteCommand, CancellationToken.None);

                Assert.True(deleteResult.IsSuccess);
            }
        }


    }
}
