using Dapper;
using Docker.DotNet.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using System.Data.Common;
using System.Reflection;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.CreateRumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.DeleteRumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.GetAllRumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.GetRumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.UpdateRumpunIlmu1;
using UnpakSipaksi.Modules.RumpunIlmu1.ApplicationTest;
using Xunit;

namespace Application.Integration.Tests
{
    public class RumpunIlmu1Test : BaseIntegrationTest
    {
        public RumpunIlmu1Test(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var empty = "";

            // Nama kosong
            yield return new object[] { empty, "'Nama' tidak boleh kosong." };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task Create_ShouldThrow_WhenInvalid(string nama, string message)
        {
            var command = new CreateRumpunIlmu1Command(nama);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            var validationError = Assert.IsType<ValidationError>(result.Error);
            Assert.Contains(validationError.Errors, e => e.Description == message);
        }

        [Fact]
        public async Task Create_ShouldAdd_WhenValid_AndCallHandler()
        {
            var command = new CreateRumpunIlmu1Command("tes");
            var result = await Sender.Send(command);

            var data = DBContext.RumpunIlmu1.FirstOrDefault(p => p.Uuid == result!.Value);
            Assert.NotNull(data);
            Assert.Equal("tes", data.Nama);

            // Assert handler bisa diresolve dari DI
            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateRumpunIlmu1Command, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateRumpunIlmu1CommandHandler>(handler);
            }
        }
        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var namaBefore = "tes";

            var updateCommand = new UpdateRumpunIlmu1Command(guid, namaBefore);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("RumpunIlmu1.NotFound", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var deleteCommand = new DeleteRumpunIlmu1Command(guid);
            var deleteResult = await Sender.Send(deleteCommand);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("RumpunIlmu1.NotFound", deleteResult.Error.Code);
        }

        [Fact]
        public async Task GetAllRumpunIlmu1_ReturnsSuccess_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new List<RumpunIlmu1Response>
        {
            new RumpunIlmu1Response { Uuid = Guid.NewGuid().ToString(), Nama = "Fokus 1" }
        };

            mockConnection.SetupDapperAsync(c => c.QueryAsync<RumpunIlmu1Response>(
                It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllRumpunIlmu1QueryHandler(mockConnectionFactory.Object);
            var query = new GetAllRumpunIlmu1Query();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
            Assert.Equal(fakeData.First().Nama, result.Value.First().Nama);
        }

        [Fact]
        public async Task GetAllRumpunIlmu1_ReturnsFailure_WhenNoData()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c => c.QueryAsync<RumpunIlmu1Response>(
                It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(new List<RumpunIlmu1Response>());

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllRumpunIlmu1QueryHandler(mockConnectionFactory.Object);
            var query = new GetAllRumpunIlmu1Query();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }


        [Fact]
        public async Task GetRumpunIlmu1_ReturnsSuccess_WhenDataExists()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new RumpunIlmu1Response { Uuid = uuid.ToString(), Nama = "Fokus 1" };

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<RumpunIlmu1Response>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetRumpunIlmu1QueryHandler(mockConnectionFactory.Object);
            var query = new GetRumpunIlmu1Query(uuid);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Nama, result.Value.Nama);
        }

        [Fact]
        public async Task GetRumpunIlmu1_ReturnsFailure_WhenDataNotFound()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<RumpunIlmu1Response>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync((RumpunIlmu1Response?)null);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetRumpunIlmu1QueryHandler(mockConnectionFactory.Object);
            var query = new GetRumpunIlmu1Query(Guid.NewGuid());

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
        }


        [Fact]
        public async Task GetRumpunIlmu1Default_ReturnsSuccess_WhenDataExists()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new RumpunIlmu1DefaultResponse { Uuid = Guid.NewGuid().ToString(), Id = "1", Nama = "Fokus Default" };

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<RumpunIlmu1DefaultResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetRumpunIlmu1DefaultQueryHandler(mockConnectionFactory.Object);
            var query = new GetRumpunIlmu1DefaultQuery(Guid.Parse(fakeData.Uuid));

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Nama, result.Value.Nama);
        }

        [Fact]
        public async Task GetRumpunIlmu1Default_ReturnsFailure_WhenDataNotFound()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<RumpunIlmu1DefaultResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync((RumpunIlmu1DefaultResponse?)null);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetRumpunIlmu1DefaultQueryHandler(mockConnectionFactory.Object);
            var query = new GetRumpunIlmu1DefaultQuery(Guid.NewGuid());

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
        }
    }
}
