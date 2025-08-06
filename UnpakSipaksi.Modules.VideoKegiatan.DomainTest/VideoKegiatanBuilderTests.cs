using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.VideoKegiatan.Domain.VideoKegiatan.VideoKegiatan;

namespace UnpakSipaksi.Modules.VideoKegiatan.DomainTest
{
    public class VideoKegiatanBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedVideoKegiatan()
        {
            // Arrange
            string Nama = "ABC";
            int Nilai = 1;

            var createResult = Domain.VideoKegiatan.VideoKegiatan.Create(
                Nama,
                Nilai
            );
            var VideoKegiatan = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = 10;

            VideoKegiatanBuilder builder = Domain.VideoKegiatan.VideoKegiatan.Update(VideoKegiatan);
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

            var createResult = Domain.VideoKegiatan.VideoKegiatan.Create(
                Nama,
                Nilai
            );
            var VideoKegiatan = createResult.Value;

            // Act
            string newNama = "ACC";
            int newNilai = -110;

            VideoKegiatanBuilder builder = Domain.VideoKegiatan.VideoKegiatan.Update(VideoKegiatan);
            builder.ChangeNama(newNama)
                    .ChangeNilai(newNilai);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("VideoKegiatan.InvalidValueNilai");
            result.Error.Description.Should().Be("Invalid value 'nilai'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
