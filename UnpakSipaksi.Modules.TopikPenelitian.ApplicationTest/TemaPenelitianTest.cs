using Microsoft.Extensions.DependencyInjection;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.TemaPenelitian.PublicApi;
using UnpakSipaksi.Modules.TopikPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.TopikPenelitian.Application.CreateTopikPenelitian;
using UnpakSipaksi.Modules.TopikPenelitian.Application.DeleteTopikPenelitian;
using UnpakSipaksi.Modules.TopikPenelitian.Application.UpdateTopikPenelitian;
using UnpakSipaksi.Modules.TopikPenelitian.ApplicationTest;
using UnpakSipaksi.Modules.TopikPenelitian.Domain.TopikPenelitian;
using Xunit;

namespace Application.Integration.Tests
{
    public class TopikPenelitianTest : BaseIntegrationTest
    {
        public TopikPenelitianTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        // INVALID DATA
        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();
            var tema = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE
            yield return new object[] { empty, "", tema, "'Nama' tidak boleh kosong.", "created" };
            yield return new object[] { empty, "tes", empty, "'TemaPenelitian' tidak boleh kosong.", "created" };
            yield return new object[] { empty, "tes", "no-guid", "'TemaPenelitian' harus dalam format UUID v4 yang valid.", "created" };

            // UPDATE
            yield return new object[] { empty, "tes", tema, "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "tes", tema, "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };
            yield return new object[] { valid, "", tema, "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { valid, "tes", empty, "'TemaPenelitian' tidak boleh kosong.", "updated" };
            yield return new object[] { valid, "tes", "no-guid", "'TemaPenelitian' harus dalam format UUID v4 yang valid.", "updated" };

            // DELETE
            yield return new object[] { empty, "tes", tema, "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "tes", tema, "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        // VALID DATA
        public static IEnumerable<object[]> ValidData()
        {
            var tema = Guid.NewGuid().ToString();

            yield return new object?[] { new object[] { "tes", tema }, null, "created" };
            yield return new object?[] { new object[] { "tes", tema }, new object[] { "tes2", tema }, "updated" };
            yield return new object?[] { new object[] { "tes", tema }, null, "deleted" };
        }

        // FLUENT VALIDATION TEST
        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalidFluentValidation(
            string uuid,
            string nama,
            string tema,
            string message,
            string mode)
        {
            Result? result = null;

            if (mode == "created")
            {
                var cmd = new CreateTopikPenelitianCommand(nama, tema);
                result = await Sender.Send(cmd);
            }
            else if (mode == "updated")
            {
                var cmd = new UpdateTopikPenelitianCommand(uuid, nama, tema);
                result = await Sender.Send(cmd);
            }
            else
            {
                var cmd = new DeleteTopikPenelitianCommand(uuid);
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
            var temaBefore = (string)before[1];

            var temaMock = new Mock<ITemaPenelitianApi>();
            temaMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new TemaPenelitianResponse("1", Guid.NewGuid().ToString(), "uuid-fokus", temaBefore));

            using var scope = Factory.Services.CreateScope();
            var services = scope.ServiceProvider;

            // CREATE
            var handler = new CreateTopikPenelitianCommandHandler(
                services.GetRequiredService<ITopikPenelitianRepository>(),
                temaMock.Object,
                services.GetRequiredService<IUnitOfWork>()
            );

            var cmd = new CreateTopikPenelitianCommand(namaBefore, temaBefore);
            var createResult = await handler.Handle(cmd, CancellationToken.None);

            Assert.True(createResult.IsSuccess);

            var newUuid = createResult.Value.ToString();

            // UPDATE
            if (mode == "updated")
            {
                var namaAfter = (string)after![0];
                var temaAfter = (string)after![1];

                var handlerUpdate = new UpdateTopikPenelitianCommandHandler(
                    services.GetRequiredService<ITopikPenelitianRepository>(),
                    temaMock.Object,
                    services.GetRequiredService<IUnitOfWork>()
                );

                var updateCmd = new UpdateTopikPenelitianCommand(newUuid, namaAfter, temaAfter);
                var updateResult = await handlerUpdate.Handle(updateCmd, CancellationToken.None);

                Assert.True(updateResult.IsSuccess);

                var dataUpdate = DBContext.TopikPenelitian.FirstOrDefault(p => p.Uuid.ToString() == newUuid);
                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);
            }

            // DELETE
            else if (mode == "deleted")
            {
                var deleteCmd = new DeleteTopikPenelitianCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCmd);

                Assert.True(deleteResult.IsSuccess);
                Assert.Null(DBContext.TopikPenelitian.FirstOrDefault(p => p.Uuid.ToString() == newUuid));
            }
        }

        // TEMA NOT FOUND
        [Fact]
        public async Task Create_ShouldThrow_WhenTemaNotExist()
        {
            var nama = "tes";
            var tema = Guid.NewGuid().ToString();

            var temaMock = new Mock<ITemaPenelitianApi>();
            temaMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((TemaPenelitianResponse?)null);

            using var scope = Factory.Services.CreateScope();
            var services = scope.ServiceProvider;

            var handler = new CreateTopikPenelitianCommandHandler(
                services.GetRequiredService<ITopikPenelitianRepository>(),
                temaMock.Object,
                services.GetRequiredService<IUnitOfWork>()
            );

            var cmd = new CreateTopikPenelitianCommand(nama, tema);
            var result = await handler.Handle(cmd, CancellationToken.None);

            Assert.True(result.IsFailure);
            Assert.Equal("TopikPenelitian.TemaPenelitianNotFound", result.Error.Code);
        }

        // DOMAIN RULE FAILURE
        [Fact]
        public async Task Create_ShouldThrow_WhenDomainRule()
        {
            var nama = "tes";
            var tema = Guid.NewGuid().ToString();

            var temaMock = new Mock<ITemaPenelitianApi>();
            temaMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new TemaPenelitianResponse("0", Guid.NewGuid().ToString(), "uuid-fokus", tema));

            using var scope = Factory.Services.CreateScope();
            var services = scope.ServiceProvider;

            var handler = new CreateTopikPenelitianCommandHandler(
                services.GetRequiredService<ITopikPenelitianRepository>(),
                temaMock.Object,
                services.GetRequiredService<IUnitOfWork>()
            );

            var cmd = new CreateTopikPenelitianCommand(nama, tema);
            var result = await handler.Handle(cmd, CancellationToken.None);

            Assert.True(result.IsFailure);
            Assert.Equal("TopikPenelitian.UnknownTemaPenelitian", result.Error.Code);
        }

        // UPDATE NOT FOUND
        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var nama = "tes";
            var tema = Guid.NewGuid().ToString();

            var temaMock = new Mock<ITemaPenelitianApi>();
            temaMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new TemaPenelitianResponse("1", Guid.NewGuid().ToString(), "uuid-fokus", tema));

            using var scope = Factory.Services.CreateScope();
            var services = scope.ServiceProvider;

            var handler = new UpdateTopikPenelitianCommandHandler(
                services.GetRequiredService<ITopikPenelitianRepository>(),
                temaMock.Object,
                services.GetRequiredService<IUnitOfWork>()
            );

            var wrongUuid = Guid.NewGuid().ToString();
            var cmd = new UpdateTopikPenelitianCommand(wrongUuid, nama, tema);

            var result = await handler.Handle(cmd, CancellationToken.None);

            Assert.True(result.IsFailure);
            Assert.Equal("TopikPenelitian.NotFound", result.Error.Code);
        }
    }
}
