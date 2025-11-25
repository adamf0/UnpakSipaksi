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
using UnpakSipaksi.Modules.Rirn.Application.CreateRirn;
using UnpakSipaksi.Modules.Rirn.Application.DeleteRirn;
using UnpakSipaksi.Modules.Rirn.Application.GetAllRirn;
using UnpakSipaksi.Modules.Rirn.Application.GetRirn;
using UnpakSipaksi.Modules.Rirn.Application.UpdateRirn;
using UnpakSipaksi.Modules.Rirn.ApplicationTest;
using Xunit;

namespace Application.Integration.Tests
{
    public class RirnTest : BaseIntegrationTest
    {
        public RirnTest(IntegrationTestWebAppFactory factory) : base(factory)
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
            var command = new CreateRirnCommand(nama);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            var validationError = Assert.IsType<ValidationError>(result.Error);
            Assert.Contains(validationError.Errors, e => e.Description == message);
        }

        [Fact]
        public async Task Create_ShouldAdd_WhenValid_AndCallHandler()
        {
            var command = new CreateRirnCommand("tes");
            var result = await Sender.Send(command);

            var data = DBContext.Rirn.FirstOrDefault(p => p.Uuid == result!.Value);
            Assert.NotNull(data);
            Assert.Equal("tes", data.Nama);

            // Assert handler bisa diresolve dari DI
            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateRirnCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateRirnCommandHandler>(handler);
            }
        }
        [Fact]
        public async Task Update_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();
            var namaBefore = "tes";

            var updateCommand = new UpdateRirnCommand(guid, namaBefore);
            var updateResult = await Sender.Send(updateCommand);

            Assert.True(updateResult.IsFailure);
            Assert.Equal("Rirn.NotFound", updateResult.Error.Code);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenNotExist()
        {
            var guid = Guid.NewGuid().ToString();

            var deleteCommand = new DeleteRirnCommand(guid);
            var deleteResult = await Sender.Send(deleteCommand);

            Assert.True(deleteResult.IsFailure);
            Assert.Equal("Rirn.NotFound", deleteResult.Error.Code);
        }

        [Fact]
        public async Task GetAllRirn_ReturnsSuccess_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new List<RirnResponse>
        {
            new RirnResponse { Uuid = Guid.NewGuid().ToString(), Nama = "Nama 1" }
        };

            mockConnection.SetupDapperAsync(c => c.QueryAsync<RirnResponse>(
                It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllRirnQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllRirnQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
            Assert.Equal(fakeData.First().Nama, result.Value.First().Nama);
        }

        [Fact]
        public async Task GetAllRirn_ReturnsFailure_WhenNoData()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c => c.QueryAsync<RirnResponse>(
                It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(new List<RirnResponse>());

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllRirnQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllRirnQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }


        [Fact]
        public async Task GetRirn_ReturnsSuccess_WhenDataExists()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new RirnResponse { Uuid = uuid.ToString(), Nama = "Nama 1" };

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<RirnResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetRirnQueryHandler(mockConnectionFactory.Object);
            var query = new GetRirnQuery(uuid);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Nama, result.Value.Nama);
        }

        [Fact]
        public async Task GetRirn_ReturnsFailure_WhenDataNotFound()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<RirnResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync((RirnResponse?)null);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetRirnQueryHandler(mockConnectionFactory.Object);
            var query = new GetRirnQuery(Guid.NewGuid());

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
        }


        [Fact]
        public async Task GetRirnDefault_ReturnsSuccess_WhenDataExists()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new RirnDefaultResponse { Uuid = Guid.NewGuid().ToString(), Id = "1", Nama = "Fokus Default" };

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<RirnDefaultResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetRirnDefaultQueryHandler(mockConnectionFactory.Object);
            var query = new GetRirnDefaultQuery(Guid.Parse(fakeData.Uuid));

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Nama, result.Value.Nama);
        }

        [Fact]
        public async Task GetRirnDefault_ReturnsFailure_WhenDataNotFound()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<RirnDefaultResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync((RirnDefaultResponse?)null);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetRirnDefaultQueryHandler(mockConnectionFactory.Object);
            var query = new GetRirnDefaultQuery(Guid.NewGuid());

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
        }
    }
}
