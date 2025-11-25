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
using UnpakSipaksi.Modules.Roadmap.Application.CreateRoadmap;
using UnpakSipaksi.Modules.Roadmap.Application.GetAllRoadmap;
using UnpakSipaksi.Modules.Roadmap.Application.GetRoadmap;
using UnpakSipaksi.Modules.Roadmap.ApplicationTest;
using Xunit;

namespace Application.Integration.Tests
{
    public class RoadmapTest : BaseIntegrationTest
    {
        public RoadmapTest(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        public static IEnumerable<object[]> InvalidData()
        {
            var validNidn = "1234567890";
            var validLink = "https://drive.google.com/file/abc123";
            var empty = "";
            var invalidLink = "https://example.com/file";

            // Nidn kosong
            yield return new object[] { empty, validLink, "'Nidn' tidak boleh kosong." };
            // Link kosong
            yield return new object[] { validNidn, empty, "'Link' tidak boleh kosong." };
            // Link tidak valid
            yield return new object[] { validNidn, invalidLink, "'Link' harus berupa URL drive.google.com yang valid dan diawali dengan http:// atau https://." };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task Create_ShouldThrow_WhenInvalid(string nidn, string link, string message)
        {
            var command = new CreateRoadmapCommand(nidn, link);
            var result = await Sender.Send(command);

            Assert.True(result.IsFailure);
            var validationError = Assert.IsType<ValidationError>(result.Error);
            Assert.Contains(validationError.Errors, e => e.Description == message);
        }

        [Fact]
        public async Task Create_ShouldAdd_WhenValid_AndCallHandler()
        {
            var command = new CreateRoadmapCommand("1234567890", "https://drive.google.com/file/xxxx");
            var result = await Sender.Send(command);

            var data = DBContext.Roadmap.FirstOrDefault(p => p.Uuid == result!.Value);
            Assert.NotNull(data);
            Assert.Equal("1234567890", data.Nidn);
            Assert.Equal("https://drive.google.com/file/xxxx", data.Link);

            // Assert handler bisa diresolve dari DI
            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreateRoadmapCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreateRoadmapCommandHandler>(handler);
            }
        }

        [Fact]
        public async Task GetAllRoadmap_ReturnsSuccess_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new List<RoadmapResponse>
        {
            new RoadmapResponse { Uuid = Guid.NewGuid().ToString(), Nidn = "1234567890", Link = "https://drive.google.com/file/xxxx" }
        };

            mockConnection.SetupDapperAsync(c => c.QueryAsync<RoadmapResponse>(
                It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllRoadmapQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllRoadmapQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
            Assert.Equal(fakeData.First().Nidn, result.Value.First().Nidn);
        }

        [Fact]
        public async Task GetAllRoadmap_ReturnsFailure_WhenNoData()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c => c.QueryAsync<RoadmapResponse>(
                It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(new List<RoadmapResponse>());

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetAllRoadmapQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllRoadmapQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }


        [Fact]
        public async Task GetRoadmap_ReturnsSuccess_WhenDataExists()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new RoadmapResponse { Uuid = uuid.ToString(), , Nidn = "1234567890", Link = "https://drive.google.com/file/xxxx" };

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<RoadmapResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync(fakeData);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetRoadmapQueryHandler(mockConnectionFactory.Object);
            var query = new GetRoadmapQuery(uuid);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.Nidn, result.Value.Nidn);
        }

        [Fact]
        public async Task GetRoadmap_ReturnsFailure_WhenDataNotFound()
        {
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<RoadmapResponse>(
                It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                .ReturnsAsync((RoadmapResponse?)null);

            mockConnectionFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConnection.Object));

            var handler = new GetRoadmapQueryHandler(mockConnectionFactory.Object);
            var query = new GetRoadmapQuery(Guid.NewGuid());

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
        }
    }
}
