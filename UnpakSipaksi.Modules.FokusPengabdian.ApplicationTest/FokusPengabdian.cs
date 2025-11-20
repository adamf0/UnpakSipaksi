using Dapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPengabdian.Application.CreateFokusPengabdian;
using UnpakSipaksi.Modules.FokusPengabdian.Application.DeleteFokusPengabdian;
using UnpakSipaksi.Modules.FokusPengabdian.Application.GetAllFokusPengabdian;
using UnpakSipaksi.Modules.FokusPengabdian.Application.GetFokusPengabdian;
using UnpakSipaksi.Modules.FokusPengabdian.Application.UpdateFokusPengabdian;
using Xunit;

namespace UnpakSipaksi.Modules.FokusPengabdian.ApplicationTest
{
    public class FokusPengabdianTest : BaseIntegrationTest
    {
        public FokusPengabdianTest(IntegrationTestWebAppFactory factory) : base(factory) { }

        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();
            var empty = "";

            // CREATE
            yield return new object[] { empty, "", "'Nama' tidak boleh kosong.", "created" };

            // UPDATE
            yield return new object[] { valid, "", "'Nama' tidak boleh kosong.", "updated" };
            yield return new object[] { empty, "tes", "'Uuid' tidak boleh kosong.", "updated" };
            yield return new object[] { "no-guid", "tes", "'Uuid' harus dalam format UUID v4 yang valid.", "updated" };

            // DELETE
            yield return new object[] { empty, "tes", "'Uuid' tidak boleh kosong.", "deleted" };
            yield return new object[] { "no-guid", "tes", "'Uuid' harus dalam format UUID v4 yang valid.", "deleted" };
        }

        public static IEnumerable<object?[]> ValidData()
        {
            yield return new object?[] { new object[] { "tes" }, null, "created" };
            yield return new object?[] { new object[] { "tes" }, new object[] { "tes2" }, "updated" };
            yield return new object?[] { new object[] { "tes" }, null, "deleted" };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task CreateUpdateDelete_ShouldThrow_WhenInvalidFluentValidation(
            string uuid,
            string nama,
            string message,
            string mode)
        {
            Result? result = null;
            if (mode == "created")
            {
                var command = new CreateFokusPengabdianCommand(nama);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateFokusPengabdianCommand(uuid, nama);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteFokusPengabdianCommand(uuid);
                result = await Sender.Send(command);
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

        [Theory]
        [MemberData(nameof(ValidData))]
        public async Task CreateUpdateDelete_ShouldBeExecute_WhenValidData(
            object[] beforeData,
            object[]? afterData,
            string mode)
        {
            // --- CREATE ---
            var namaBefore = (string)beforeData[0];

            var createCommand = new CreateFokusPengabdianCommand(namaBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.FokusPengabdian.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateFokusPengabdianCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateFokusPengabdianCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // --- UPDATE / DELETE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];

                var updateCommand = new UpdateFokusPengabdianCommand(newUuid, namaAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.FokusPengabdian.FirstOrDefault(p => p.Uuid.ToString() == createResult!.Value.ToString());

                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateFokusPengabdianCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<UpdateFokusPengabdianCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeleteFokusPengabdianCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);
            }
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var namaBefore = "tes";

            var updateCommand = new UpdateFokusPengabdianCommand(guid, namaBefore);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("FokusPengabdian.NotFound", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var deleteCommand = new DeleteFokusPengabdianCommand(guid);
            var deleteResult = await Sender.Send(deleteCommand);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("FokusPengabdian.NotFound", deleteResult.Error.Code);
        }

        [Fact]
        public async Task GetAllFokusPengabdian_ReturnsSuccess_WhenDataExists()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new List<FokusPengabdianResponse>
            {
                new FokusPengabdianResponse { Uuid = Guid.NewGuid().ToString(), Nama = "Pengabdian 1" }
            };

            mockConnection.SetupDapperAsync(c => c.QueryAsync<FokusPengabdianResponse>(
                It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllFokusPengabdianQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllFokusPengabdianQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
            Assert.Equal(fakeData.First().Nama, result.Value.First().Nama);
        }

        [Fact]
        public async Task GetAllFokusPengabdian_ReturnsFailure_WhenNoData()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c => c.QueryAsync<FokusPengabdianResponse>(
                It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(new List<FokusPengabdianResponse>());

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllFokusPengabdianQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllFokusPengabdianQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetFokusPengabdian_ReturnsSuccess_WhenDataExists()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new FokusPengabdianResponse { Uuid = uuid.ToString(), Nama = "Pengabdian 1" };

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<FokusPengabdianResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetFokusPengabdianQueryHandler(mockConnectionFactory.Object);
            var query = new GetFokusPengabdianQuery(uuid);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Nama, result.Value.Nama);
        }

        [Fact]
        public async Task GetFokusPengabdian_ReturnsFailure_WhenDataNotFound()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<FokusPengabdianResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync((FokusPengabdianResponse?)null);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetFokusPengabdianQueryHandler(mockConnectionFactory.Object);
            var query = new GetFokusPengabdianQuery(Guid.NewGuid());

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetFokusPengabdianDefault_ReturnsSuccess_WhenDataExists()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new FokusPengabdianDefaultResponse { Uuid = Guid.NewGuid().ToString(), Nama = "Pengabdian Default" };

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<FokusPengabdianDefaultResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetFokusPengabdianDefaultQueryHandler(mockConnectionFactory.Object);
            var query = new GetFokusPengabdianDefaultQuery(Guid.Parse(fakeData.Uuid));

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Nama, result.Value.Nama);
        }

        [Fact]
        public async Task GetFokusPengabdianDefault_ReturnsFailure_WhenDataNotFound()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<FokusPengabdianDefaultResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync((FokusPengabdianDefaultResponse?)null);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetFokusPengabdianDefaultQueryHandler(mockConnectionFactory.Object);
            var query = new GetFokusPengabdianDefaultQuery(Guid.NewGuid());

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
        }
    }
}
