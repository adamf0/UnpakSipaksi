using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KebaruanReferensi.Application.GetKebaruanReferensi;
using UnpakSipaksi.Modules.KebaruanReferensi.Application.GetBobotKebaruanReferensi;
using UnpakSipaksi.Modules.KebaruanReferensi.Infrastructure.PublicApi;
using Xunit;
using UnpakSipaksi.Modules.KebaruanReferensi.Domain.KebaruanReferensi;

public sealed class KebaruanReferensiApiTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly KebaruanReferensiApi _api;

    public KebaruanReferensiApiTests()
    {
        _senderMock = new Mock<ISender>();
        _api = new KebaruanReferensiApi(_senderMock.Object);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnNull_WhenFailure()
    {
        // Arrange
        Guid uuid = Guid.NewGuid();

        _senderMock
    .Setup(s => s.Send(It.IsAny<GetKebaruanReferensiDefaultQuery>(), It.IsAny<CancellationToken>()))
    .Returns((GetKebaruanReferensiDefaultQuery q, CancellationToken ct) =>
        Task.FromResult(Result.Failure<KebaruanReferensiDefaultResponse>(KebaruanReferensiErrors.NotFound(Guid.NewGuid()))));

        // Act
        var result = await _api.GetAsync(uuid);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnMappedResponse_WhenSuccess()
    {
        // Arrange
        Guid uuid = Guid.NewGuid();

        var response = new KebaruanReferensiDefaultResponse
        {
            Id = "10",
            Uuid = uuid.ToString(),
            Nama = "Inovasi A",
            BobotPDP = 20,
            BobotTerapan = 30,
            BobotKerjasama = 40,
            BobotPenelitianDasar = 50,
            Skor = 85
        };

        _senderMock
            .Setup(s => s.Send(It.IsAny<GetKebaruanReferensiDefaultQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(response));

        // Act
        var result = await _api.GetAsync(uuid);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("10", result!.Id);
        Assert.Equal(uuid.ToString(), result.Uuid);
        Assert.Equal("Inovasi A", result.Nama);
        Assert.Equal(20, result.BobotPDP);
        Assert.Equal(30, result.BobotTerapan);
        Assert.Equal(40, result.BobotKerjasama);
        Assert.Equal(50, result.BobotPenelitianDasar);
        Assert.Equal(85, result.Skor);
    }

    [Fact]
    public async Task GetBobotWithoutTargetAsync_ShouldReturnNull_WhenFailure()
    {
        // Arrange
        _senderMock
            .Setup(s => s.Send(It.IsAny<GetBobotKebaruanReferensiQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<int?>(KebaruanReferensiErrors.UnknownKategoriSkema()));

        // Act
        var result = await _api.GetBobotWithoutTargetAsync("Penelitian Dasar");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetBobotWithoutTargetAsync_ShouldReturnValue_WhenSuccess()
    {
        // Arrange
        _senderMock
            .Setup(s => s.Send(It.IsAny<GetBobotKebaruanReferensiQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<int?>(70));

        // Act
        var result = await _api.GetBobotWithoutTargetAsync("PDP");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(70, result);
    }
}
