using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.GetBobotKesesuaianSolusiMasalahMitra;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.GetKesesuaianSolusiMasalahMitra;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Domain.KesesuaianSolusiMasalahMitra;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Infrastructure.PublicApi;
using Xunit;

public sealed class KesesuaianSolusiMasalahMitraApiTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly KesesuaianSolusiMasalahMitraApi _api;

    public KesesuaianSolusiMasalahMitraApiTests()
    {
        _senderMock = new Mock<ISender>();
        _api = new KesesuaianSolusiMasalahMitraApi(_senderMock.Object);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnNull_WhenFailure()
    {
        // Arrange
        Guid uuid = Guid.NewGuid();

        _senderMock
            .Setup(s => s.Send(It.IsAny<GetKesesuaianSolusiMasalahMitraDefaultQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<KesesuaianSolusiMasalahMitraDefaultResponse>(
                KesesuaianSolusiMasalahMitraErrors.NotFound(uuid)
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
        var response = new KesesuaianSolusiMasalahMitraDefaultResponse
        {
            Id = "12",
            Uuid = uuid.ToString(),
            Nama = "Artikel Jurnal",
            Nilai = 88
        };

        _senderMock
            .Setup(s => s.Send(It.IsAny<GetKesesuaianSolusiMasalahMitraDefaultQuery>(), It.IsAny<CancellationToken>()))
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
            .Setup(s => s.Send(It.IsAny<GetBobotKesesuaianSolusiMasalahMitraQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<int?>(
                KesesuaianSolusiMasalahMitraErrors.UnknownKategoriSkema()
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
            .Setup(s => s.Send(It.IsAny<GetBobotKesesuaianSolusiMasalahMitraQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<int?>(75));

        // Act
        var result = await _api.GetBobotWithoutTargetAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(75, result);
    }
}
