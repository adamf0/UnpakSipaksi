using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using MediatR;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.GetPeningkatanKeberdayaanMitra;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.GetBobotPeningkatanKeberdayaanMitra;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.PublicApi;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Domain.PeningkatanKeberdayaanMitra;

public class PeningkatanKeberdayaanMitraApiTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly PeningkatanKeberdayaanMitraApi _api;

    public PeningkatanKeberdayaanMitraApiTests()
    {
        _senderMock = new Mock<ISender>();
        _api = new PeningkatanKeberdayaanMitraApi(_senderMock.Object);
    }

    [Fact]
    public async Task GetAsync_WhenSuccess_ReturnsResponse()
    {
        // Arrange
        Guid uuid = Guid.NewGuid();

        var response = new PeningkatanKeberdayaanMitraDefaultResponse
        {
            Id = "1",
            Uuid = uuid.ToString(),
            Nama = "Peningkatan A",
            Nilai = 88
        };

        _senderMock
            .Setup(s => s.Send(
                It.Is<GetPeningkatanKeberdayaanMitraDefaultQuery>(q => q.PeningkatanKeberdayaanMitraUuid == uuid),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(response));

        // Act
        var result = await _api.GetAsync(uuid);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("1", result.Id);
        Assert.Equal(uuid.ToString(), result.Uuid);
        Assert.Equal("Peningkatan A", result.Nama);
        Assert.Equal(88, result.Nilai);
    }

    [Fact]
    public async Task GetAsync_WhenFailure_ReturnsNull()
    {
        // Arrange
        Guid uuid = Guid.NewGuid();

        _senderMock
            .Setup(s => s.Send(
                It.IsAny<GetPeningkatanKeberdayaanMitraDefaultQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<PeningkatanKeberdayaanMitraDefaultResponse>(PeningkatanKeberdayaanMitraErrors.NotFound(Guid.NewGuid())));

        // Act
        var result = await _api.GetAsync(uuid);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetBobotWithoutTargetAsync_WhenSuccess_ReturnsValue()
    {
        // Arrange
        _senderMock
            .Setup(s => s.Send(
                It.IsAny<GetBobotPeningkatanKeberdayaanMitraQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<int?>(99));

        // Act
        var result = await _api.GetBobotWithoutTargetAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(99, result);
    }

    [Fact]
    public async Task GetBobotWithoutTargetAsync_WhenFailure_ReturnsNull()
    {
        // Arrange
        _senderMock
            .Setup(s => s.Send(
                It.IsAny<GetBobotPeningkatanKeberdayaanMitraQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<int?>(PeningkatanKeberdayaanMitraErrors.UnknownKategoriSkema()));

        // Act
        var result = await _api.GetBobotWithoutTargetAsync();

        // Assert
        Assert.Null(result);
    }
}
