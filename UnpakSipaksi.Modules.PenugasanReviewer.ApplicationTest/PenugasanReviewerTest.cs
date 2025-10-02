using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenugasanReviewer.Application.CreatePenugasanReviewer;
using Xunit;

namespace UnpakSipaksi.Modules.PenugasanReviewer.ApplicationTest
{
    public class PenugasanReviewerTest : BaseIntegrationTest
    {
        public PenugasanReviewerTest(IntegrationTestWebAppFactory factory) : base(factory)
        {

        }

        [Fact]
        public async Task Create_ShouldThrow_WhenNamaKosong_ByFluentValidation()
        {
            // Arrange
            var command = new CreatePenugasanReviewerCommand("", 1);

            // Act
            var result = await Sender.Send(command);

            // Assert
            Assert.True(result.IsFailure);
            var validationError = Assert.IsType<ValidationError>(result.Error);
            Assert.Contains(validationError.Errors, e => e.Description == "'Nidn' tidak boleh kosong.");
        }

        [Fact]
        public async Task Create_ShouldThrow_WhenStatusKosong_ByFluentValidation()
        {
            // Arrange
            var command = new CreatePenugasanReviewerCommand("1234567890", -100);

            // Act
            var result = await Sender.Send(command);

            // Assert
            Assert.True(result.IsFailure);
            var validationError = Assert.IsType<ValidationError>(result.Error);
            Assert.Contains(validationError.Errors, e => e.Description == "'Status' format tidak diketahui.");
        }

        [Fact]
        public async Task Create_ShouldAdd_WhenValid_AndCallHandler()
        {
            var command = new CreatePenugasanReviewerCommand("1234567890", 1);

            var result = await Sender.Send(command);

            var data = DBContext.PenugasanReviewer.FirstOrDefault(p => p.Uuid == result!.Value);

            Assert.NotNull(data);
            Assert.Equal("1234567890", data.Nidn);
            Assert.Equal(1, data.Status);

            // Assert bahwa handler bisa diresolve dari DI
            using (var scope = Factory.Services.CreateScope())
            {
                var handler = scope.ServiceProvider
                    .GetService<IRequestHandler<CreatePenugasanReviewerCommand, Result<Guid>>>();

                Assert.NotNull(handler);
                Assert.IsType<CreatePenugasanReviewerCommandHandler>(handler);
            }
        }
    }
}
