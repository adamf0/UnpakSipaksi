using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.GetBobotModelFeasibilityStudys;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.GetModelFeasibilityStudys;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Domain.ModelFeasibilityStudys;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.PublicApi;
using Xunit;

public class ModelFeasibilityStudysApiTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly ModelFeasibilityStudysApi _api;

    public ModelFeasibilityStudysApiTests()
    {
        _senderMock = new Mock<ISender>();
        _api = new ModelFeasibilityStudysApi(_senderMock.Object);
    }

    [Fact]
    public async Task GetAsync_WhenSuccess_ReturnsResponse()
    {
        // Arrange
        Guid uuid = Guid.NewGuid();

        var response = new ModelFeasibilityStudysDefaultResponse
        {
            Id = "1",
            Uuid = uuid.ToString(),
            Nama = "Feasibility A",
            BobotPDP = 10,
            BobotTerapan = 20,
            BobotKerjasama = 30,
            BobotPenelitianDasar = 40,
            Skor = 100
        };

        _senderMock
            .Setup(s => s.Send(
                It.Is<GetModelFeasibilityStudysDefaultQuery>(q => q.ModelFeasibilityStudysUuid == uuid),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(response));

        // Act
        var result = await _api.GetAsync(uuid);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("1", result.Id);
        Assert.Equal(uuid.ToString(), result.Uuid);
        Assert.Equal("Feasibility A", result.Nama);
        Assert.Equal(10, result.BobotPDP);
        Assert.Equal(20, result.BobotTerapan);
        Assert.Equal(30, result.BobotKerjasama);
        Assert.Equal(40, result.BobotPenelitianDasar);
        Assert.Equal(100, result.Skor);
    }

    [Fact]
    public async Task GetAsync_WhenFailure_ReturnsNull()
    {
        // Arrange
        Guid uuid = Guid.NewGuid();

        _senderMock
            .Setup(s => s.Send(
                It.IsAny<GetModelFeasibilityStudysDefaultQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<ModelFeasibilityStudysDefaultResponse>(ModelFeasibilityStudysErrors.NotFound(Guid.NewGuid())));

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
                It.IsAny<GetBobotModelFeasibilityStudysQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<int?>(75));

        // Act
        var result = await _api.GetBobotWithoutTargetAsync("PDP");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(75, result);
    }

    [Fact]
    public async Task GetBobotWithoutTargetAsync_WhenFailure_ReturnsNull()
    {
        // Arrange
        _senderMock
            .Setup(s => s.Send(
                It.IsAny<GetBobotModelFeasibilityStudysQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<int?>(ModelFeasibilityStudysErrors.UnknownKategoriSkema()));

        // Act
        var result = await _api.GetBobotWithoutTargetAsync("PDP");

        // Assert
        Assert.Null(result);
    }
}
