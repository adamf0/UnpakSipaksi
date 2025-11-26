using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.GetBobotPublikasiDisitasiProposal;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.GetPublikasiDisitasiProposal;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Domain.PublikasiDisitasiProposal;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Infrastructure.PublicApi;
using Xunit;

public class PublikasiDisitasiProposalApiTests
{
    [Fact]
    public async Task GetAsync_Returns_Response_When_Success()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        var api = new PublikasiDisitasiProposalApi(mockSender.Object);

        var uuid = Guid.NewGuid();

        var response = new PublikasiDisitasiProposalDefaultResponse
        {
            Id = "PDSP-001",
            Uuid = uuid.ToString(),
            Nama = "Publikasi Disitasi A",
            BobotPDP = 5,
            BobotTerapan = 10,
            BobotKerjasama = 15,
            BobotPenelitianDasar = 20,
            Skor = 50
        };

        mockSender
            .Setup(s => s.Send(It.IsAny<GetPublikasiDisitasiProposalDefaultQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success(response));

        // Act
        var result = await api.GetAsync(uuid);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("PDSP-001", result.Id);
        Assert.Equal(uuid.ToString(), result.Uuid);
        Assert.Equal("Publikasi Disitasi A", result.Nama);
        Assert.Equal(5, result.BobotPDP);
        Assert.Equal(10, result.BobotTerapan);
        Assert.Equal(15, result.BobotKerjasama);
        Assert.Equal(20, result.BobotPenelitianDasar);
        Assert.Equal(50, result.Skor);
    }

    [Fact]
    public async Task GetAsync_Returns_Null_When_Failure()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        var api = new PublikasiDisitasiProposalApi(mockSender.Object);

        mockSender
            .Setup(s => s.Send(It.IsAny<GetPublikasiDisitasiProposalDefaultQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<PublikasiDisitasiProposalDefaultResponse>(PublikasiDisitasiProposalErrors.NotFound(Guid.NewGuid())));

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
        var api = new PublikasiDisitasiProposalApi(mockSender.Object);

        mockSender
            .Setup(s => s.Send(It.IsAny<GetBobotPublikasiDisitasiProposalQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<int?>(85));

        // Act
        var result = await api.GetBobotWithoutTargetAsync("PDP");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(85, result);
    }

    [Fact]
    public async Task GetBobotWithoutTargetAsync_Returns_Null_When_Failure()
    {
        // Arrange
        var mockSender = new Mock<ISender>();
        var api = new PublikasiDisitasiProposalApi(mockSender.Object);

        mockSender
            .Setup(s => s.Send(It.IsAny<GetBobotPublikasiDisitasiProposalQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<int?>(PublikasiDisitasiProposalErrors.UnknownKategoriSkema()));

        // Act
        var result = await api.GetBobotWithoutTargetAsync("PDP");

        // Assert
        Assert.Null(result);
    }
}
