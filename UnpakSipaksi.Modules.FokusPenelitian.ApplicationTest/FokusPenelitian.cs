using Dapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPenelitian.Application.CreateFokusPenelitian;
using UnpakSipaksi.Modules.FokusPenelitian.Application.DeleteFokusPenelitian;
using UnpakSipaksi.Modules.FokusPenelitian.Application.GetAllFokusPenelitian;
using UnpakSipaksi.Modules.FokusPenelitian.Application.GetFokusPenelitian;
using UnpakSipaksi.Modules.FokusPenelitian.Application.UpdateFokusPenelitian;
using Xunit;

namespace UnpakSipaksi.Modules.FokusPenelitian.ApplicationTest
{
    public class FokusPenelitianTest : BaseIntegrationTest
    {
        public FokusPenelitianTest(IntegrationTestWebAppFactory factory) : base(factory) { }

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
                var command = new CreateFokusPenelitianCommand(nama);
                result = await Sender.Send(command);
            }
            else if (mode == "updated")
            {
                var command = new UpdateFokusPenelitianCommand(uuid, nama);
                result = await Sender.Send(command);
            }
            else
            {
                var command = new DeleteFokusPenelitianCommand(uuid);
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

            var createCommand = new CreateFokusPenelitianCommand(namaBefore);
            var createResult = await Sender.Send(createCommand);

            Assert.True(createResult.IsSuccess);
            var dataCreate = DBContext.FokusPenelitian.FirstOrDefault(p => p.Uuid == createResult!.Value);

            Assert.NotNull(dataCreate);
            Assert.Equal(namaBefore, dataCreate.Nama);

            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateFokusPenelitianCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateFokusPenelitianCommandHandler>(handler);
            }

            var newUuid = createResult.Value.ToString();

            // --- UPDATE / DELETE ---
            if (mode == "updated")
            {
                var namaAfter = (string)afterData![0];

                var updateCommand = new UpdateFokusPenelitianCommand(newUuid, namaAfter);
                var updateResult = await Sender.Send(updateCommand);

                Assert.True(updateResult.IsSuccess);
                var dataUpdate = DBContext.FokusPenelitian.FirstOrDefault(p => p.Uuid == createResult!.Value);

                Assert.NotNull(dataUpdate);
                Assert.Equal(namaAfter, dataUpdate.Nama);

                using (var scope = Factory.Services.CreateScope())
                {
                    var handler = scope.ServiceProvider
                        .GetService<IRequestHandler<UpdateFokusPenelitianCommand, Result>>();

                    Assert.NotNull(handler);
                    Assert.IsType<UpdateFokusPenelitianCommandHandler>(handler);
                }
            }
            else if (mode == "deleted")
            {
                var deleteCommand = new DeleteFokusPenelitianCommand(newUuid);
                var deleteResult = await Sender.Send(deleteCommand);

                Assert.True(deleteResult.IsSuccess);
            }
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var namaBefore = "tes";

            var updateCommand = new UpdateFokusPenelitianCommand(guid, namaBefore);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("FokusPenelitian.NotFound", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var deleteCommand = new DeleteFokusPenelitianCommand(guid);
            var deleteResult = await Sender.Send(deleteCommand);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("FokusPenelitian.NotFound", deleteResult.Error.Code);
        }

        [Fact]
        public async Task GetAllFokusPenelitian_ReturnsSuccess_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new List<FokusPenelitianResponse>
        {
            new FokusPenelitianResponse { Uuid = Guid.NewGuid().ToString(), Nama = "Fokus 1" }
        };

            mockConnection.SetupDapperAsync(c => c.QueryAsync<FokusPenelitianResponse>(
                It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllFokusPenelitianQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllFokusPenelitianQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
            Assert.Equal(fakeData.First().Nama, result.Value.First().Nama);
        }

        [Fact]
        public async Task GetAllFokusPenelitian_ReturnsFailure_WhenNoData()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c => c.QueryAsync<FokusPenelitianResponse>(
                It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(new List<FokusPenelitianResponse>());

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllFokusPenelitianQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllFokusPenelitianQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }


        [Fact]
        public async Task GetFokusPenelitian_ReturnsSuccess_WhenDataExists()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new FokusPenelitianResponse { Uuid = uuid.ToString(), Nama = "Fokus 1" };

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<FokusPenelitianResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetFokusPenelitianQueryHandler(mockConnectionFactory.Object);
            var query = new GetFokusPenelitianQuery(uuid);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Nama, result.Value.Nama);
        }

        [Fact]
        public async Task GetFokusPenelitian_ReturnsFailure_WhenDataNotFound()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<FokusPenelitianResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync((FokusPenelitianResponse?)null);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetFokusPenelitianQueryHandler(mockConnectionFactory.Object);
            var query = new GetFokusPenelitianQuery(Guid.NewGuid());

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
        }


        [Fact]
        public async Task GetFokusPenelitianDefault_ReturnsSuccess_WhenDataExists()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new FokusPenelitianDefaultResponse { Uuid = Guid.NewGuid().ToString(), Id = "1", Nama = "Fokus Default" };

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<FokusPenelitianDefaultResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetFokusPenelitianDefaultQueryHandler(mockConnectionFactory.Object);
            var query = new GetFokusPenelitianDefaultQuery(Guid.Parse(fakeData.Uuid));

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Nama, result.Value.Nama);
        }

        [Fact]
        public async Task GetFokusPenelitianDefault_ReturnsFailure_WhenDataNotFound()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<FokusPenelitianDefaultResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync((FokusPenelitianDefaultResponse?)null);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetFokusPenelitianDefaultQueryHandler(mockConnectionFactory.Object);
            var query = new GetFokusPenelitianDefaultQuery(Guid.NewGuid());

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
        }
    }
}
