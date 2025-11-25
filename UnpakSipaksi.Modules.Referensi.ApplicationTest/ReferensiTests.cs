using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KebaruanReferensi.PublicApi;
using UnpakSipaksi.Modules.Referensi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Referensi.Application.CreateReferensi;
using UnpakSipaksi.Modules.Referensi.Application.GetAllReferensi;
using UnpakSipaksi.Modules.Referensi.Application.GetReferensi;
using UnpakSipaksi.Modules.Referensi.Domain.Referensi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.Referensi.ApplicationTest
{
    public class ReferensiTests : BaseIntegrationTest
    {
        public ReferensiTests(IntegrationTestWebAppFactory factory)
            : base(factory) { }

        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();

            yield return new object[] { "", valid, valid, 100, "'Nama' tidak boleh kosong." };
            // Empty
            yield return new object[] { "tes", "", valid, 100, "'KebaruanReferensi' tidak boleh kosong." };
            yield return new object[] { "tes", valid, "", 100, "'RelevansiKualitasReferensi' tidak boleh kosong." };

            // Invalid GUID
            yield return new object[] { "tes", "not-guid", valid, 100, "'KebaruanReferensi' harus dalam format UUID v4 yang valid." };
            yield return new object[] { "tes", valid, "not-guid", 100, "'RelevansiKualitasReferensi' harus dalam format UUID v4 yang valid." };
        }

        public static IEnumerable<object[]> validData()
        {
            var valid = Guid.NewGuid().ToString();

            // Invalid GUID
            yield return new object[] { "tes", valid, valid, 100 };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task Handle_ShouldReturnFailure_WhenValidationFails(
            string nama,
            string kebaharuanReferensi,
            string relevansiKualitasReferensi,
            int nilai,
            string expectedMessage)
        {
            // Arrange
            var command = new CreateReferensiCommand(
                nama,
                kebaharuanReferensi,
                relevansiKualitasReferensi,
                nilai
            );

            // Act
            var result = await Sender.Send(command);

            // Assert
            Assert.True(result.IsFailure);
            var validationError = Assert.IsType<ValidationError>(result.Error);
            Assert.Contains(validationError.Errors, e => e.Description == expectedMessage);
        }

        [Theory]
        [MemberData(nameof(validData))]
        public async Task Handle_ShouldReturnSuccess_WhenValidData(
            string nama,
            string kebaharuanReferensi,
            string relevansiKualitasReferensi,
            int nilai)
        {
            // Arrange – semua API dimock agar return dummy valid
            var kebaharuanReferensiMock = new Mock<IKebaruanReferensiApi>();
            kebaharuanReferensiMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new KebaruanReferensiResponse("1", Guid.NewGuid().ToString(), "ok", 1, 1, 1, 1, 10));

            var relevansiKualitasReferensiMock = new Mock<IRelevansiKualitasReferensiApi>();
            relevansiKualitasReferensiMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RelevansiKualitasReferensiResponse("2", Guid.NewGuid().ToString(), "ok", 1, 1, 1, 1, 10));

            var handler = new CreateReferensiCommandHandler(
                GetService<IReferensiRepository>(),
                kebaharuanReferensiMock.Object,
                relevansiKualitasReferensiMock.Object,
                GetService<IUnitOfWork>()
            );

            var command = new CreateReferensiCommand(
                nama,
                kebaharuanReferensi,
                relevansiKualitasReferensi,
                nilai
            );

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.True(result.IsSuccess);
        }

        private T GetService<T>() where T : notnull
        {
            return Factory.Services.CreateScope().ServiceProvider.GetRequiredService<T>();
        }

        [Fact]
        public async Task GetAll_ReturnsList_WhenDataExists()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            var fakeData = new List<ReferensiResponse>
            {
                new ReferensiResponse
                {
                    Uuid = Guid.NewGuid().ToString(),
                    UuidKebaruanReferensi = Guid.NewGuid().ToString(),
                    UuidRelevansiKualitasReferensi = Guid.NewGuid().ToString(),
                    Nilai = 10
                }
            };

            mockConn.SetupDapperAsync(c => c.QueryAsync<ReferensiResponse>(
                It.IsAny<string>(), null, null, null, null
            )).ReturnsAsync(fakeData);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConn.Object));

            var handler = new GetAllReferensiQueryHandler(mockFactory.Object);
            var query = new GetAllReferensiQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
            Assert.Equal(fakeData.First().UuidKebaruanReferensi, result.Value.First().UuidKebaruanReferensi);
            Assert.Equal(fakeData.First().UuidRelevansiKualitasReferensi, result.Value.First().UuidRelevansiKualitasReferensi);
        }

        [Fact]
        public async Task GetAll_ReturnsFailure_WhenNoData()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c => c.QueryAsync<ReferensiResponse>(
                It.IsAny<string>(), null, null, null, null
            )).ReturnsAsync(new List<ReferensiResponse>());

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConn.Object));

            var handler = new GetAllReferensiQueryHandler(mockFactory.Object);
            var query = new GetAllReferensiQuery();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetByUuid_ReturnsSuccess_WhenDataFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new ReferensiResponse
            {
                Uuid = uuid.ToString(),
                UuidKebaruanReferensi = Guid.NewGuid().ToString(),
                UuidRelevansiKualitasReferensi = Guid.NewGuid().ToString(),
                Nilai = 10
            };

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<ReferensiResponse?>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
            )).ReturnsAsync(fakeData);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConn.Object));

            var handler = new GetReferensiQueryHandler(mockFactory.Object);
            var query = new GetReferensiQuery(uuid);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.UuidKebaruanReferensi, result.Value.UuidKebaruanReferensi);
            Assert.Equal(fakeData.UuidRelevansiKualitasReferensi, result.Value.UuidRelevansiKualitasReferensi);
        }

        [Fact]
        public async Task GetByUuid_ReturnsFailure_WhenNotFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<ReferensiResponse?>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
            )).ReturnsAsync((ReferensiResponse?)null);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConn.Object));

            var handler = new GetReferensiQueryHandler(mockFactory.Object);
            var query = new GetReferensiQuery(Guid.NewGuid();

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetDefault_ReturnsSuccess_WhenDataFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new ReferensiDefaultResponse
            {
                Id = "1",
                Uuid = uuid.ToString(),
                UuidKebaruanReferensi = Guid.NewGuid().ToString(),
                UuidRelevansiKualitasReferensi = Guid.NewGuid().ToString(),
                Nilai = 10
            };

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<ReferensiDefaultResponse?>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
            )).ReturnsAsync(fakeData);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConn.Object));

            var handler = new GetReferensiDefaultQueryHandler(mockFactory.Object);
            var query = new GetReferensiDefaultQuery(uuid);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(fakeData.UuidKebaruanReferensi, result.Value.UuidKebaruanReferensi);
            Assert.Equal(fakeData.UuidRelevansiKualitasReferensi, result.Value.UuidRelevansiKualitasReferensi);
        }

        [Fact]
        public async Task GetDefault_ReturnsFailure_WhenNotFound()
        {
            var mockFactory = new Mock<IDbConnectionFactory>();
            var mockConn = new Mock<DbConnection>();

            mockConn.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<ReferensiDefaultResponse?>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
            )).ReturnsAsync((ReferensiDefaultResponse?)null);

            mockFactory.Setup(f => f.OpenConnectionAsync())
                .Returns(new ValueTask<DbConnection>(mockConn.Object));

            var handler = new GetReferensiDefaultQueryHandler(mockFactory.Object);
            var query = new GetReferensiDefaultQuery(Guid.NewGuid());

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.False(result.IsSuccess);
        }
    }
}
