using Docker.DotNet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPenelitian.PublicApi;
using UnpakSipaksi.Modules.TemaPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.TemaPenelitian.Application.CreateTemaPenelitian;
using UnpakSipaksi.Modules.TemaPenelitian.Application.DeleteTemaPenelitian;
using UnpakSipaksi.Modules.TemaPenelitian.Application.UpdateTemaPenelitian;
using UnpakSipaksi.Modules.TemaPenelitian.ApplicationTest;
using UnpakSipaksi.Modules.TemaPenelitian.Domain.TemaPenelitian;
using Xunit;

namespace Application.Integration.Tests
{
    public class TemaPenelitianTest : BaseIntegrationTest
    {
        public TemaPenelitianTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        // INVALID DATA
        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();
            var fokus = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE
            yield return new object[] { empty, "", fokus, "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { empty, "tes", empty, "'FokusPenelitian' tidak boleh kosong.", "created" };
            yield return new object[] { empty, "tes", "no-guid", "'FokusPenelitian' harus dalam format UUID v4 yang valid.", "created" };

            // UPDATE
            yield return new object[] { empty, "tes", fokus, "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "tes", fokus, "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };
            yield return new object[] { valid, "", fokus, "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { valid, "tes", empty, "'FokusPenelitian' tidak boleh kosong.", "updated" };
            yield return new object[] { valid, "tes", "no-guid", "'FokusPenelitian' harus dalam format UUID v4 yang valid.", "updated" };

            // DELETE
            yield return new object[] { empty, "tes", fokus, "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "tes", fokus, "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        // VALID DATA
        public static IEnumerable<object[]> ValidData()
        {
            var fokus = Guid.NewGuid().ToString();

            yield return new object?[] { new object[] { "tes", fokus }, null, "created" };
            yield return new object?[] { new object[] { "tes", fokus }, new object[] { "tes2", fokus }, "updated" };
            yield return new object?[] { new object[] { "tes", fokus }, null, "deleted" };
        }

        // FLUENT VALIDATION TEST
        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalidFluentValidation(
            string uuid,
            string nama,
            string fokus,
            string message,
            string mode)
        {
            Result? result = null;

            if (mode == "created")
            {
                var cmd = new CreateTemaPenelitianCommand(nama, fokus);
                result = await Sender.Send(cmd);
            }
            else if (mode == "updated")
            {
                var cmd = new UpdateTemaPenelitianCommand(uuid, nama, fokus);
                result = await Sender.Send(cmd);
            }
            else
            {
                var cmd = new DeleteTemaPenelitianCommand(uuid);
                result = await Sender.Send(cmd);
            }

            Assert.True(result.IsFailure);

            if (result.Error is ValidationError validationError)
            {
                Assert.Contains(validationError.Errors, e => e.Description == message);
            }
            else
            {
                Assert.Equal(message, result.Error.Description);
            }
        }

        // MAIN VALID EXECUTION TEST
        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task CreateUpdateDelete_ShouldExecute_WhenValid(
            object[] before,
            object[]? after,
            string mode)
        {
            var namaBefore = (string)before[0];
            var fokusBefore = (string)before[1];

            var fokusMock = new Mock<IFokusPenelitianApi>();
            fokusMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FokusPenelitianResponse("1", Guid.NewGuid().ToString(), fokusBefore));

            using var scope = Factory.Services.CreateScope();
            var services = scope.ServiceProvider;

            // CREATE HANDLER
            var handler = new CreateTemaPenelitianCommandHandler(
                services.GetRequiredService<ITemaPenelitianRepository>(),
                fokusMock.Object,
                services.GetRequiredService<IUnitOfWork>()
            );

            var cmd = new CreateTemaPenelitianCommand(namaBefore, fokusBefore);
            var createResult = await handler.Handle(cmd, CancellationToken.None);

            Assert.True(createResult.IsSuccess);

            var newUuid = createResult.Value.ToString();

            // UPDATE
            if (mode == "updated")
            {
                var namaAfter = (string)after![0];
                var fokusAfter = (string)after![1];

                var handlerUpdate = new UpdateTemaPenelitianCommandHandler(
                    services.GetRequiredService<ITemaPenelitianRepository>(),
                    fokusMock.Object,
                    services.GetRequiredService<IUnitOfWork>()
                );

                var updateCmd = new UpdateTemaPenelitianCommand(newUuid, namaAfter, fokusAfter);
                var updateResult = await handlerUpdate.Handle(updateCmd, CancellationToken.None);

                Assert.True(updateResult.IsSuccess);

                var dataUpdate = DBContext.TemaPenelitian.FirstOrDefault(p => p.Uuid.ToString() == newUuid);
                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
            }

            // DELETE
            else if (mode == "deleted")
            {
                var deleteCmd = new DeleteTemaPenelitianCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCmd);

                Assert.True(deleteResult.IsSuccess);
                Assert.Null(DBContext.TemaPenelitian.FirstOrDefault(p => p.Uuid.ToString() == newUuid));
            }
        }

        // FOKUS NOT FOUND
        [Fact]
        public async Task Create_ShouldThrow_WhenFokusNotExist()
        {
            var nama = "tes";
            var fokus = Guid.NewGuid().ToString();

            var mock = new Mock<IFokusPenelitianApi>();
            mock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((FokusPenelitianResponse?)null);

            using var scope = Factory.Services.CreateScope();
            var services = scope.ServiceProvider;

            var handler = new CreateTemaPenelitianCommandHandler(
                services.GetRequiredService<ITemaPenelitianRepository>(),
                mock.Object,
                services.GetRequiredService<IUnitOfWork>()
            );

            var cmd = new CreateTemaPenelitianCommand(nama, fokus);
            var result = await handler.Handle(cmd, CancellationToken.None);

            Assert.True(result.IsFailure);
            Assert.Equal("TemaPenelitian.FokusPenelitianNotFound", result.Error.Code);
        }

        // DOMAIN RULE FAILURE
        [Fact]
        public async Task Create_ShouldThrow_WhenDomainRule()
        {
            var nama = "tes";
            var fokus = Guid.NewGuid().ToString();

            var mock = new Mock<IFokusPenelitianApi>();
            mock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FokusPenelitianResponse("0", fokus, "fokus"));

            using var scope = Factory.Services.CreateScope();
            var services = scope.ServiceProvider;

            var handler = new CreateTemaPenelitianCommandHandler(
                services.GetRequiredService<ITemaPenelitianRepository>(),
                mock.Object,
                services.GetRequiredService<IUnitOfWork>()
            );

            var cmd = new CreateTemaPenelitianCommand(nama, fokus);
            var result = await handler.Handle(cmd, CancellationToken.None);

            Assert.True(result.IsFailure);
            Assert.Equal("TemaPenelitian.UnknownFokusPenelitian", result.Error.Code);
        }

        // UPDATE NOT FOUND
        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var nama = "tes";
            var fokus = Guid.NewGuid().ToString();

            var mock = new Mock<IFokusPenelitianApi>();
            mock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FokusPenelitianResponse("1", fokus, "fokus"));

            using var scope = Factory.Services.CreateScope();
            var services = scope.ServiceProvider;

            var handler = new UpdateTemaPenelitianCommandHandler(
                services.GetRequiredService<ITemaPenelitianRepository>(),
                mock.Object,
                services.GetRequiredService<IUnitOfWork>()
            );

            var wrongUuid = Guid.NewGuid().ToString();
            var cmd = new UpdateTemaPenelitianCommand(wrongUuid, nama, fokus);

            var result = await handler.Handle(cmd, CancellationToken.None);

            Assert.True(result.IsFailure);
            Assert.Equal("TemaPenelitian.NotFound", result.Error.Code);
        }
    }
}
