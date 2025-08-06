using FluentAssertions;
using Moq;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.Substansi;
using Xunit;

namespace UnpakSipaksi.Modules.PenelitianPkm.DomainTest.Substansi
{
    public class SubstansiTests
    {
        private async Task<Domain.PenelitianPkm.PenelitianPkm> CreateHibahAsync()
        {
            var mockRepo = new Mock<IPenelitianPkmRepository>();
            mockRepo.Setup(x =>
                x.HasUniqueDataAsync(
                    It.IsAny<Guid?>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()
                )).ReturnsAsync(true);

            var hibahResult = await Domain.PenelitianPkm.PenelitianPkm.Create(
                mockRepo.Object,
                "123",
                "2024-01-01",
                "judul"
            );

            hibahResult.IsSuccess.Should().BeTrue();

            var hibah = hibahResult.Value;
            hibah.GetType()?.GetProperty("Id")?.SetValue(hibah, 1);

            return hibah;
        }

        [Theory]
        [InlineData(null, "file.pdf", "NotFoundHibah")] // hibah null
        [InlineData("valid", null, "EmptyFile")]       // file null
        [InlineData("valid", "", "EmptyFile")]         // file empty
        public async Task Create_ShouldReturnFailure_WhenInvalidInput(
            string? hibahState,
            string? file,
            string expectedErrorCode)
        {
            Domain.PenelitianPkm.PenelitianPkm? hibah = null;
            if (hibahState == "valid")
            {
                hibah = await CreateHibahAsync();
            }

            var result = Domain.Substansi.Substansi.Create(Guid.NewGuid(), hibah, file);

            result.IsFailure.Should().BeTrue();

            switch (expectedErrorCode)
            {
                case "NotFoundHibah":
                    result.Error.Code.Should().Be("Substansi.NotFoundHibah");
                    break;
                case "EmptyFile":
                    result.Error.Code.Should().Be("Substansi.EmptyFile");
                    break;
                case "NotFound":
                    result.Error.Code.Should().Be("Substansi.NotFound");
                    break;
                default:
                    Assert.Fail("Unexpected error code");
                    break;
            }

        }

        [Fact]
        public async Task Create_ShouldReturnSuccess_WhenInputIsValid()
        {
            var hibah = await CreateHibahAsync();
            var result = Domain.Substansi.Substansi.Create(Guid.NewGuid(), hibah, "file.pdf");

            result.IsSuccess.Should().BeTrue();
            result.Value.File.Should().Be("file.pdf");
            result.Value.PenelitianPkmId.Should().Be(hibah.Id);
            result.Value.Uuid.Should().NotBeEmpty();
        }

        [Theory]
        [InlineData(null, "valid", "file.pdf", "NotFound")]        // prev null
        [InlineData("valid", null, "file.pdf", "NotFound")]        // hibah null
        [InlineData("valid", "valid", null, "EmptyFile")]          // file null
        [InlineData("valid", "valid", "", "EmptyFile")]            // file empty
        public async Task Update_ShouldReturnFailure_WhenInvalidInput(
            string? prevState,
            string? hibahState,
            string? file,
            string expectedErrorCode
        )
        {
            Domain.PenelitianPkm.PenelitianPkm? hibah = null;
            Domain.Substansi.Substansi? prev = null;

            if (hibahState == "valid")
            {
                hibah = await CreateHibahAsync();
            }

            if (prevState == "valid" && hibah != null)
            {
                prev = Domain.Substansi.Substansi.Create(Guid.NewGuid(), hibah, "file.pdf").Value;
            }

            var result = Domain.Substansi.Substansi.Update(Guid.NewGuid(), Guid.NewGuid(), hibah, prev, file);

            result.IsFailure.Should().BeTrue();

            switch (expectedErrorCode)
            {
                case "NotFoundHibah":
                    result.Error.Code.Should().Be("Substansi.NotFoundHibah");
                    break;
                case "EmptyFile":
                    result.Error.Code.Should().Be("Substansi.EmptyFile");
                    break;
                case "NotFound":
                    result.Error.Code.Should().Be("Substansi.NotFound");
                    break;
                default:
                    Assert.Fail("Unexpected error code");
                    break;
            }
        }

        [Fact]
        public async Task Update_ShouldReturnSuccess_WhenInputIsValid()
        {
            var hibah = await CreateHibahAsync();
            var prev = Domain.Substansi.Substansi.Create(Guid.NewGuid(), hibah, "oldfile.pdf").Value;

            var newFile = "newfile.pdf";
            var result = Domain.Substansi.Substansi.Update(Guid.NewGuid(), Guid.NewGuid(), hibah, prev, newFile);

            result.IsSuccess.Should().BeTrue();
            result.Value.File.Should().Be(newFile);
            ReferenceEquals(result.Value, prev).Should().BeTrue();
        }
    }
}
