using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Dapper;
using System.Data.Common;
using UnpakSipaksi.Common.Application.Data;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AkurasiPenelitian.PublicApi;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.PublicApi;
using UnpakSipaksi.Modules.KesesuaianTkt.PublicApi;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.PublicApi;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.PublicApi;
using UnpakSipaksi.Modules.Metode.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Metode.Application.CreateMetode;
using UnpakSipaksi.Modules.Metode.Application.GetAllMetode;
using UnpakSipaksi.Modules.Metode.Application.GetMetode;
using UnpakSipaksi.Modules.Metode.Domain.Metode;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.PublicApi;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.Metode.ApplicationTest
{
    public class MetodeTest : BaseIntegrationTest
    {
        public MetodeTest(IntegrationTestWebAppFactory factory)
            : base(factory) { }

        public static IEnumerable<object[]> InvalidData()
        {
            var valid = Guid.NewGuid().ToString();
            var emty = "";

            // Empty
            yield return new object[] { "", valid, valid, valid, valid, valid, valid, "'AkurasiPenelitian' tidak boleh kosong." };
            yield return new object[] { valid, "", valid, valid, valid, valid, valid, "'KejelasanPembagianTugasTim' tidak boleh kosong." };
            yield return new object[] { valid, valid, "", valid, valid, valid, valid, "'KesesuaianWaktuRabLuaranFasilitas' tidak boleh kosong." };
            yield return new object[] { valid, valid, valid, "", valid, valid, valid, "'PotensiKetercapaianLuaranDijanjikan' tidak boleh kosong." };
            yield return new object[] { valid, valid, valid, valid, "", valid, valid, "'ModelFeasibilityStudy' tidak boleh kosong." };
            yield return new object[] { valid, valid, valid, valid, valid, "", valid, "'KesesuaianTkt' tidak boleh kosong." };
            yield return new object[] { valid, valid, valid, valid, valid, valid, "", "'KredibilitasMitraDukungan' tidak boleh kosong." };

            // Invalid GUID
            yield return new object[] { "not-guid", valid, valid, valid, valid, valid, valid, "'AkurasiPenelitian' harus dalam format UUID v4 yang valid." };
            yield return new object[] { valid, "not-guid", valid, valid, valid, valid, valid, "'KejelasanPembagianTugasTim' harus dalam format UUID v4 yang valid." };
            yield return new object[] { valid, valid, "not-guid", valid, valid, valid, valid, "'KesesuaianWaktuRabLuaranFasilitas' harus dalam format UUID v4 yang valid." };
            yield return new object[] { valid, valid, valid, "not-guid", valid, valid, valid, "'PotensiKetercapaianLuaranDijanjikan' harus dalam format UUID v4 yang valid." };
            yield return new object[] { valid, valid, valid, valid, "not-guid", valid, valid, "'ModelFeasibilityStudy' harus dalam format UUID v4 yang valid." };
            yield return new object[] { valid, valid, valid, valid, valid, "not-guid", valid, "'KesesuaianTkt' harus dalam format UUID v4 yang valid." };
            yield return new object[] { valid, valid, valid, valid, valid, valid, "not-guid", "'KredibilitasMitraDukungan' harus dalam format UUID v4 yang valid." };
        }

        public static IEnumerable<object[]> validData()
        {
            var valid = Guid.NewGuid().ToString();

            // Invalid GUID
            yield return new object[] { valid, valid, valid, valid, valid, valid, valid };
        }

        [Theory]
        [MemberData(nameof(InvalidData))]
        public async Task Handle_ShouldReturnFailure_WhenValidationFails(
            string akurasi,
            string tugasTim,
            string waktu,
            string potensi,
            string model,
            string tkt,
            string mitra,
            string expectedMessage)
        {
            // Arrange
            var command = new CreateMetodeCommand(
                akurasi,
                tugasTim,
                waktu,
                potensi,
                model,
                tkt,
                mitra
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
            string akurasi,
            string tugasTim,
            string waktu,
            string potensi,
            string model,
            string tkt,
            string mitra)
        {
            // Arrange – semua API dimock agar return dummy valid
            var akurasiMock = new Mock<IAkurasiPenelitianApi>();
            akurasiMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AkurasiPenelitianResponse("1", akurasi, "ok", 1, 1, 1, 1, 10));

            var tugasTimMock = new Mock<IKejelasanPembagianTugasTimApi>();
            tugasTimMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new KejelasanPembagianTugasTimResponse("2", tugasTim, "ok", 1, 1, 1, 1, 10));

            var waktuMock = new Mock<IKesesuaianWaktuRabLuaranFasilitasApi>();
            waktuMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new KesesuaianWaktuRabLuaranFasilitasResponse("3", waktu, "ok", 1, 1, 1, 1, 10));

            var potensiMock = new Mock<IPotensiKetercapaianLuaranDijanjikanApi>();
            potensiMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PotensiKetercapaianLuaranDijanjikanResponse("4", potensi, "ok", 1, 1, 1, 1, 10));

            var modelMock = new Mock<IModelFeasibilityStudysApi>();
            modelMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ModelFeasibilityStudysResponse("5", model, "ok", 1, 1, 1, 1, 10));

            var tktMock = new Mock<IKesesuaianTktApi>();
            tktMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new KesesuaianTktResponse("6", tkt, "ok", 1, 1, 1, 1, 10));

            var mitraMock = new Mock<IKredibilitasMitraDukunganApi>();
            mitraMock.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new KredibilitasMitraDukunganResponse("7", mitra, "ok", 1, 1, 1, 1, 10));

            var handler = new CreateMetodeCommandHandler(
                GetService<IMetodeRepository>(),
                akurasiMock.Object,
                tugasTimMock.Object,
                waktuMock.Object,
                potensiMock.Object,
                modelMock.Object,
                tktMock.Object,
                mitraMock.Object,
                GetService<IUnitOfWork>()
            );

            var command = new CreateMetodeCommand(
                akurasi,
                tugasTim,
                waktu,
                potensi,
                model,
                tkt,
                mitra
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
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            var fakeData = new List<MetodeResponse>
        {
            new MetodeResponse
            {
                Uuid = Guid.NewGuid().ToString(),
                UuidAkurasiPenelitian = Guid.NewGuid().ToString(),
                UuidKejelasanPembagianTugasTim = Guid.NewGuid().ToString(),
                UuidKesesuaianWaktuRabLuaranFasilitas = Guid.NewGuid().ToString(),
                UuidPotensiKetercapaianLuaranDijanjikan = Guid.NewGuid().ToString(),
                UuidModelFeasibilityStudy = Guid.NewGuid().ToString(),
                UuidKesesuaianTkt = Guid.NewGuid().ToString(),
                UuidKredibilitasMitraDukungan = Guid.NewGuid().ToString()
            }
        };

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<MetodeResponse>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(fakeData);

            mockConnectionFactory
                .Setup(x => x.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetAllMetodeQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllMetodeQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Value);
        }

        [Fact]
        public async Task GetAll_ReturnsFailure_WhenNoData()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QueryAsync<MetodeResponse>(It.IsAny<string>(), null, null, null, null)
            ).ReturnsAsync(new List<MetodeResponse>());

            mockConnectionFactory
                .Setup(x => x.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetAllMetodeQueryHandler(mockConnectionFactory.Object);
            var query = new GetAllMetodeQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetByUuid_ReturnsSuccess_WhenDataExists()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();
            var uuid = Guid.NewGuid();

            var fakeData = new MetodeResponse
            {
                Uuid = uuid.ToString(),
                UuidAkurasiPenelitian = Guid.NewGuid().ToString(),
                UuidKejelasanPembagianTugasTim = Guid.NewGuid().ToString(),
                UuidKesesuaianWaktuRabLuaranFasilitas = Guid.NewGuid().ToString(),
                UuidPotensiKetercapaianLuaranDijanjikan = Guid.NewGuid().ToString(),
                UuidModelFeasibilityStudy = Guid.NewGuid().ToString(),
                UuidKesesuaianTkt = Guid.NewGuid().ToString(),
                UuidKredibilitasMitraDukungan = Guid.NewGuid().ToString()
            };

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<MetodeResponse?>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync(fakeData);

            mockConnectionFactory
                .Setup(x => x.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetMetodeQueryHandler(mockConnectionFactory.Object);
            var query = new GetMetodeQuery(uuid);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(uuid.ToString(), result.Value.Uuid);
        }

        [Fact]
        public async Task GetByUuid_ReturnsFailure_WhenNotFound()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockConnection = new Mock<DbConnection>();

            mockConnection.SetupDapperAsync(c =>
                c.QuerySingleOrDefaultAsync<MetodeResponse?>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null
                )
            ).ReturnsAsync((MetodeResponse?)null);

            mockConnectionFactory
                .Setup(x => x.OpenConnectionAsync())
                .ReturnsAsync(mockConnection.Object);

            var handler = new GetMetodeQueryHandler(mockConnectionFactory.Object);
            var query = new GetMetodeQuery(Guid.NewGuid());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
