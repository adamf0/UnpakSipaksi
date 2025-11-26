using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.GetBobotKewajaranTahapanTarget;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.GetKewajaranTahapanTarget;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Domain.KewajaranTahapanTarget;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Infrastructure.PublicApi;
using Xunit;

public sealed class KewajaranTahapanTargetApiTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly KewajaranTahapanTargetApi _api;

    public KewajaranTahapanTargetApiTests()
    {
        _senderMock = new Mock<ISender>();
        _api = new KewajaranTahapanTargetApi(_senderMock.Object);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnNull_WhenFailure()
    {
        // Arrange
        Guid uuid = Guid.NewGuid();

        _senderMock
            .Setup(s => s.Send(It.IsAny<GetKewajaranTahapanTargetDefaultQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<KewajaranTahapanTargetDefaultResponse>(
                KewajaranTahapanTargetErrors.NotFound(uuid)
            ));

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

        // HARUS PROPERTY INIT (karena Anda error: "does not have parameter named Id")
        var response = new KewajaranTahapanTargetDefaultResponse
        {
            Id = "12",
            Uuid = uuid.ToString(),
            Nama = "Artikel Jurnal",
            Nilai = 88
        };

        _senderMock
            .Setup(s => s.Send(It.IsAny<GetKewajaranTahapanTargetDefaultQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(response));

        // Act
        var result = await _api.GetAsync(uuid);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("12", result.Id);
        Assert.Equal(uuid.ToString(), result.Uuid);
        Assert.Equal("Artikel Jurnal", result.Nama);
        Assert.Equal(88, result.Nilai);
    }

    [Fact]
    public async Task GetBobotWithoutTargetAsync_ShouldReturnNull_WhenFailure()
    {
        // Arrange
        _senderMock
            .Setup(s => s.Send(It.IsAny<GetBobotKewajaranTahapanTargetQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<int?>(
                KewajaranTahapanTargetErrors.UnknownKategoriSkema()
            ));

        // Act
        var result = await _api.GetBobotWithoutTargetAsync();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetBobotWithoutTargetAsync_ShouldReturnValue_WhenSuccess()
    {
        // Arrange
        _senderMock
            .Setup(s => s.Send(It.IsAny<GetBobotKewajaranTahapanTargetQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<int?>(75));

        // Act
        var result = await _api.GetBobotWithoutTargetAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(75, result);
    }
}
