using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.GetBobotMetodeRencanaKegiatan;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.GetMetodeRencanaKegiatan;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Domain.MetodeRencanaKegiatan;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Infrastructure.PublicApi;
using Xunit;

public sealed class MetodeRencanaKegiatanApiTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly MetodeRencanaKegiatanApi _api;

    public MetodeRencanaKegiatanApiTests()
    {
        _senderMock = new Mock<ISender>();
        _api = new MetodeRencanaKegiatanApi(_senderMock.Object);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnNull_WhenResultIsFailure()
    {
        // Arrange
        Guid uuid = Guid.NewGuid();

        _senderMock
            .Setup(s => s.Send(It.IsAny<GetMetodeRencanaKegiatanDefaultQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<MetodeRencanaKegiatanDefaultResponse>(MetodeRencanaKegiatanErrors.NotFound(Guid.NewGuid())));

        // Act
        var result = await _api.GetAsync(uuid);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnMappedResponse_WhenResultSuccess()
    {
        // Arrange
        Guid uuid = Guid.NewGuid();

        // DefaultResponse Anda jelas BUKAN positional, jadi harus pakai object initializer
        var response = new MetodeRencanaKegiatanDefaultResponse
        {
            Id = "10",
            Uuid = uuid.ToString(),
            Nama = "Metode A",
            Nilai = 99
        };

        _senderMock
            .Setup(s => s.Send(It.IsAny<GetMetodeRencanaKegiatanDefaultQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(response));

        // Act
        var result = await _api.GetAsync(uuid);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("10", result!.Id);
        Assert.Equal(uuid.ToString(), result.Uuid);
        Assert.Equal("Metode A", result.Nama);
        Assert.Equal(99, result.Nilai);
    }

    [Fact]
    public async Task GetBobotWithoutTargetAsync_ShouldReturnNull_WhenFailure()
    {
        _senderMock
            .Setup(s => s.Send(It.IsAny<GetBobotMetodeRencanaKegiatanQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<int?>(MetodeRencanaKegiatanErrors.UnknownKategoriSkema()));

        var result = await _api.GetBobotWithoutTargetAsync();

        Assert.Null(result);
    }

    [Fact]
    public async Task GetBobotWithoutTargetAsync_ShouldReturnValue_WhenSuccess()
    {
        _senderMock
            .Setup(s => s.Send(It.IsAny<GetBobotMetodeRencanaKegiatanQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<int?>(70));

        var result = await _api.GetBobotWithoutTargetAsync();

        Assert.NotNull(result);
        Assert.Equal(70, result);
    }
}
