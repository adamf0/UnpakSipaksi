using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using static UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Domain.RelevansiProdukKepentinganNasional.RelevansiProdukKepentinganNasional;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.DomainTest
{
    public class RelevansiProdukKepentinganNasionalBuilderTests
    {
        [Fact]
        public void Build_WhenFieldsChanged_ShouldReturnUpdatedRelevansiProdukKepentinganNasional()
        {
            // Arrange
            string Nama = "ABC";
            int Skor = 1;

            var createResult = Domain.RelevansiProdukKepentinganNasional.RelevansiProdukKepentinganNasional.Create(
                Nama,
                Skor
            );
            var RelevansiProdukKepentinganNasional = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = 10;

            RelevansiProdukKepentinganNasionalBuilder builder = Domain.RelevansiProdukKepentinganNasional.RelevansiProdukKepentinganNasional.Update(RelevansiProdukKepentinganNasional);
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

            var createResult = Domain.RelevansiProdukKepentinganNasional.RelevansiProdukKepentinganNasional.Create(
                Nama,
                Skor
            );
            var RelevansiProdukKepentinganNasional = createResult.Value;

            // Act
            string newNama = "ACC";
            int newSkor = -110;

            RelevansiProdukKepentinganNasionalBuilder builder = Domain.RelevansiProdukKepentinganNasional.RelevansiProdukKepentinganNasional.Update(RelevansiProdukKepentinganNasional);
            builder.ChangeNama(newNama)
                    .ChangeSkor(newSkor);
            var result = builder.Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("RelevansiProdukKepentinganNasional.InvalidValueSkor");
            result.Error.Description.Should().Be("Invalid value 'skor'");
            result.Error.Type.Should().Be(ErrorType.NotFound);
        }
    }
}
