using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.GetBobotRelevansiKepakaranTemaProposal;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.GetRelevansiKepakaranTemaProposal;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Infrastructure.PublicApi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Domain.RelevansiKualitasReferensi;
using Xunit;

public class RelevansiKepakaranTemaProposalApiTests
{
    [Fact]
    public async Task GetAsync_Returns_Response_When_Success()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        var api = new RelevansiKepakaranTemaProposalApi(mockSender.Object);

        var uuid = Guid.NewGuid();

        var response = new RelevansiKepakaranTemaProposalDefaultResponse
        {
            Id = "RKP-123",
            Uuid = uuid.ToString(),
            Nama = "Tema Proposal Test",
            BobotPDP = 10,
            BobotTerapan = 20,
            BobotKerjasama = 30,
            BobotPenelitianDasar = 40,
            Skor = 100
        };

        mockSender
            .Setup(s => s.Send(It.IsAny<GetRelevansiKepakaranTemaProposalDefaultQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(response));

        // Act
        var result = await api.GetAsync(uuid);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("RKP-123", result.Id);
        Assert.Equal(uuid.ToString(), result.Uuid);
        Assert.Equal("Tema Proposal Test", result.Nama);
        Assert.Equal(10, result.BobotPDP);
        Assert.Equal(20, result.BobotTerapan);
        Assert.Equal(30, result.BobotKerjasama);
        Assert.Equal(40, result.BobotPenelitianDasar);
        Assert.Equal(100, result.Skor);
    }

    [Fact]
    public async Task GetAsync_Returns_Null_When_Failure()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        var api = new RelevansiKepakaranTemaProposalApi(mockSender.Object);

        mockSender
            .Setup(s => s.Send(It.IsAny<GetRelevansiKepakaranTemaProposalDefaultQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<RelevansiKepakaranTemaProposalDefaultResponse>(RelevansiKualitasReferensiErrors.NotFound(Guid.NewGuid())));

        // Act
        var result = await api.GetAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetBobotWithoutTargetAsync_Returns_Value_When_Success()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        var api = new RelevansiKepakaranTemaProposalApi(mockSender.Object);

        mockSender
            .Setup(s => s.Send(It.IsAny<GetBobotRelevansiKepakaranTemaProposalQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<int?>(70));

        // Act
        var result = await api.GetBobotWithoutTargetAsync("PDP");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(70, result);
    }

    [Fact]
    public async Task GetBobotWithoutTargetAsync_Returns_Null_When_Failure()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        var api = new RelevansiKepakaranTemaProposalApi(mockSender.Object);

        mockSender
            .Setup(s => s.Send(It.IsAny<GetBobotRelevansiKepakaranTemaProposalQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<int?>(RelevansiKualitasReferensiErrors.UnknownKategoriSkema()));

        // Act
        var result = await api.GetBobotWithoutTargetAsync("PDP");

        // Assert
        Assert.Null(result);
    }
}
