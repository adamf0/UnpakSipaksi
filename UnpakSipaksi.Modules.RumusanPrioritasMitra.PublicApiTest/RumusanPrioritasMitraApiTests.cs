using MediatR;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.GetBobotRumusanPrioritasMitra;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.GetRumusanPrioritasMitra;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Domain.RumusanPrioritasMitra;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Infrastructure.PublicApi;
using Xunit;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Tests
{
    public class RumusanPrioritasMitraApiTests
    {
        private readonly Mock<ISender> _senderMock;
        private readonly RumusanPrioritasMitraApi _api;

        public RumusanPrioritasMitraApiTests()
        {
            _senderMock = new Mock<ISender>();
            _api = new RumusanPrioritasMitraApi(_senderMock.Object);
        }


        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenFailure()
        {
            _senderMock
                .Setup(s => s.Send(It.IsAny<GetRumusanPrioritasMitraDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure<RumusanPrioritasMitraDefaultResponse>(RumusanPrioritasMitraErrors.NotFound(Guid.NewGuid())));

            var result = await _api.GetAsync(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnResponse_WhenSuccess()
        {
            var uuid = Guid.NewGuid().ToString();

            var response = new RumusanPrioritasMitraDefaultResponse
            {
                Id = "10",
                Uuid = uuid,
                Nama = "Contoh Rumusan",
                Nilai = 80
            };

            _senderMock
                .Setup(s => s.Send(It.IsAny<GetRumusanPrioritasMitraDefaultQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(response));

            var result = await _api.GetAsync(Guid.Parse(uuid));

            Assert.NotNull(result);
            Assert.Equal("10", result.Id);
            Assert.Equal(uuid, result.Uuid);
            Assert.Equal("Contoh Rumusan", result.Nama);
            Assert.Equal(80, result.Nilai);
        }

        [Fact]
        public async Task GetBobotWithoutTargetAsync_ShouldReturnNull_WhenFailure()
        {
            _senderMock
                .Setup(s => s.Send(It.IsAny<GetBobotRumusanPrioritasMitraQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure<int?>(RumusanPrioritasMitraErrors.NotSameValue()));

            var result = await _api.GetBobotWithoutTargetAsync();

            Assert.Null(result);
        }

        [Fact]
        public async Task GetBobotWithoutTargetAsync_ShouldReturnValue_WhenSuccess()
        {
            _senderMock
                .Setup(s => s.Send(It.IsAny<GetBobotRumusanPrioritasMitraQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success<int?>(45));

            var result = await _api.GetBobotWithoutTargetAsync();

            Assert.NotNull(result);
            Assert.Equal(45, result);
        }
    }
}
