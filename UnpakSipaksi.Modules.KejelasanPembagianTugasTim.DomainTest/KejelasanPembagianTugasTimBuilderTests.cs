using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Domain.KejelasanPembagianTugasTim.KejelasanPembagianTugasTim;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.DomainTest
{
    public class KejelasanPembagianTugasTimBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedKejelasanPembagianTugasTim()
        {
            // Arrange
            string Nama = "ABC";
            int Skor = 1;

            var createResult = Domain.KejelasanPembagianTugasTim.KejelasanPembagianTugasTim.Create(
                Nama,
                Skor
            );
            var KejelasanPembagianTugasTim = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = 10;

            KejelasanPembagianTugasTimBuilder builder = Domain.KejelasanPembagianTugasTim.KejelasanPembagianTugasTim.Update(KejelasanPembagianTugasTim);
            builder.ChangeNama(newNama)
                    .ChangeSkor(newSkor);
            var result = builder.Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Nama.Should().Be(newNama);
            result.Value.Skor.Should().Be(newSkor);
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Build_WithInvalidSkor_ShouldReturnExpectedValidationError()
        {
            // Arrange
            string Nama = "ABC";
            int Skor = 1;

            var createResult = Domain.KejelasanPembagianTugasTim.KejelasanPembagianTugasTim.Create(
                Nama,
                Skor
            );
            var KejelasanPembagianTugasTim = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = -110;

            KejelasanPembagianTugasTimBuilder builder = Domain.KejelasanPembagianTugasTim.KejelasanPembagianTugasTim.Update(KejelasanPembagianTugasTim);
            builder.ChangeNama(newNama)
                    .ChangeSkor(newSkor);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("KejelasanPembagianTugasTim.InvalidValueSkor");
            result.Error.Description.Should().Be("Invalid value 'skor'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
