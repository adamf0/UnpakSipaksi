using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Domain.PeningkatanKeberdayaanMitra.PeningkatanKeberdayaanMitra;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.DomainTest
{
    public class PeningkatanKeberdayaanMitraBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedPeningkatanKeberdayaanMitra()
        {
            // Arrange
            string Nama = "ABC";
            int Nilai = 1;

            var createResult = Domain.PeningkatanKeberdayaanMitra.PeningkatanKeberdayaanMitra.Create(
                Nama,
                Nilai
            );
            var PeningkatanKeberdayaanMitra = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = 10;

            PeningkatanKeberdayaanMitraBuilder builder = Domain.PeningkatanKeberdayaanMitra.PeningkatanKeberdayaanMitra.Update(PeningkatanKeberdayaanMitra);
            builder.ChangeNama(newNama)
                    .ChangeNilai(newNilai);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(newNama);
            result.Value.Nilai.Should().Be(newNilai);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Build_WithInvalidNilai_ShouldReturnExpectedValidationError()
        {
            // Arrange
            string Nama = "ABC";
            int Nilai = 1;

            var createResult = Domain.PeningkatanKeberdayaanMitra.PeningkatanKeberdayaanMitra.Create(
                Nama,
                Nilai
            );
            var PeningkatanKeberdayaanMitra = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = -110;

            PeningkatanKeberdayaanMitraBuilder builder = Domain.PeningkatanKeberdayaanMitra.PeningkatanKeberdayaanMitra.Update(PeningkatanKeberdayaanMitra);
            builder.ChangeNama(newNama)
                    .ChangeNilai(newNilai);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("PeningkatanKeberdayaanMitra.InvalidValueNilai");
            result.Error.Description.Should().Be("Invalid value 'nilai'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
