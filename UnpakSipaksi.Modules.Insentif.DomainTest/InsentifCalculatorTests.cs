using FluentAssertions;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Insentif.Domain.CheckInsentif;
using UnpakSipaksi.Modules.Insentif.Domain.Insentif;
using UnpakSipaksi.Modules.Insentif.Domain.VerifikasiFakultas;

namespace UnpakSipaksi.Modules.Insentif.DomainTest
{
    public class InsentifCalculatorTests
    {
        private const decimal Q1SBU = 20000000;
        private const decimal Q2SBU = 15000000;
        private const decimal Q3SBU = 10000000;
        private const decimal Q4SBU = 7500000;
        private const decimal S1SBU = 7500000;
        private const decimal S2SBU = 5000000;
        private const decimal S3SBU = 4000000;
        private const decimal S4SBU = 2500000;
        private const decimal S5SBU = 750000;
        private const decimal InternasionalSBU = 5000000;
        private const decimal IssnSBU = 500000;
        private const decimal ProsidingSBU = 3000000;

        [Theory]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 1, 12000000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 2, 12000000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 3, 12000000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 4, 12000000)]

        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 2, 8000000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 3, 4000000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 4, 2666666.6666666665)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 1, 6000000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 2, 6000000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 3, 6000000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 4, 6000000)]

        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 2, 4000000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 3, 2000000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 4, 1333333.3333333333)]

        ////
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 1, 6000000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 2, 6000000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 3, 6000000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 4, 6000000)]

        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 2, 4000000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 3, 2000000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 4, 1333333.3333333333)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 1, 3000000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 2, 3000000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 3, 3000000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 4, 3000000)]

        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 2, 2000000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 3, 1000000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 4, 666666.6666666666)]
        public void Q1_Combinations_Should_Calculate_Correctly(JenisJurnal jenisJurnal, bool mahasiswa, Peran peran, int jumlahCA, decimal expected)
        {
            // Arrange
            var input = new InsentifInput
            {
                JenisJurnal = jenisJurnal,
                Mahasiswa = mahasiswa,
                PeranPenulis = peran,
                SBU = Q1SBU,
                JumlahCoAuthor = jumlahCA
            };

            // Act
            var result = InsentifCalculator.Hitung(input);
            result.IsSuccess.Should().BeTrue();

            // Assert
            Assert.Equal(expected, result.Value.TotalInsentif, 6);
        }



        [Theory]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 1, 9000000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 2, 9000000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 3, 9000000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 4, 9000000)]

        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 2, 6000000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 3, 3000000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 4, 2000000)]

        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 1, 4500000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 2, 4500000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 3, 4500000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 4, 4500000)]

        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 2, 3000000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 3, 1500000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 4, 1000000)]

        ///
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 1, 4500000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 2, 4500000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 3, 4500000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 4, 4500000)]

        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 2, 3000000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 3, 1500000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 4, 1000000)]

        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 1, 2250000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 2, 2250000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 3, 2250000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 4, 2250000)]

        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 2, 1500000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 3, 750000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 4, 500000)]
        public void Q2_Combinations_Should_Calculate_Correctly(JenisJurnal jenisJurnal, bool mahasiswa, Peran peran, int jumlahCA, decimal expected)
        {
            // Arrange
            var input = new InsentifInput
            {
                JenisJurnal = jenisJurnal,
                Mahasiswa = mahasiswa,
                PeranPenulis = peran,
                SBU = Q2SBU,
                JumlahCoAuthor = jumlahCA
            };

            // Act
            var result = InsentifCalculator.Hitung(input);
            result.IsSuccess.Should().BeTrue();

            // Assert
            Assert.Equal(expected, result.Value.TotalInsentif, 6);
        }

        [Theory]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 1, 6000000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 2, 6000000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 3, 6000000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 4, 6000000)]

        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 2, 4000000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 3, 2000000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 4, 1333333.333333)]

        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 1, 3000000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 2, 3000000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 3, 3000000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 4, 3000000)]

        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 2, 2000000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 3, 1000000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 4, 666666.666667)]

        ///
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 1, 3000000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 2, 3000000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 3, 3000000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 4, 3000000)]

        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 2, 2000000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 3, 1000000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 4, 666666.666667)]

        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 1, 1500000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 2, 1500000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 3, 1500000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 4, 1500000)]

        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 2, 1000000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 3, 500000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 4, 333333.333333)]
        public void Q3_Combinations_Should_Calculate_Correctly(JenisJurnal jenisJurnal, bool mahasiswa, Peran peran, int jumlahCA, decimal expected)
        {
            // Arrange
            var input = new InsentifInput
            {
                JenisJurnal = jenisJurnal,
                Mahasiswa = mahasiswa,
                PeranPenulis = peran,
                SBU = Q3SBU,
                JumlahCoAuthor = jumlahCA
            };

            // Act
            var result = InsentifCalculator.Hitung(input);
            result.IsSuccess.Should().BeTrue();

            // Assert
            Assert.Equal(expected, result.Value.TotalInsentif, 6);
        }


        [Theory]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 1, 4500000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 2, 4500000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 3, 4500000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 4, 4500000)]

        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 2, 3000000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 3, 1500000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 4, 1000000)]

        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 1, 2250000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 2, 2250000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 3, 2250000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 4, 2250000)]

        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 2, 1500000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 3, 750000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 4, 500000)]

        ///
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 1, 2250000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 2, 2250000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 3, 2250000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 4, 2250000)]

        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 2, 1500000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 3, 750000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 4, 500000)]

        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 1, 1125000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 2, 1125000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 3, 1125000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 4, 1125000)]

        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 2, 750000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 3, 375000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 4, 250000)]
        public void Q4_Combinations_Should_Calculate_Correctly(JenisJurnal jenisJurnal, bool mahasiswa, Peran peran, int jumlahCA, decimal expected)
        {
            // Arrange
            var input = new InsentifInput
            {
                JenisJurnal = jenisJurnal,
                Mahasiswa = mahasiswa,
                PeranPenulis = peran,
                SBU = Q4SBU,
                JumlahCoAuthor = jumlahCA
            };

            // Act
            var result = InsentifCalculator.Hitung(input);
            result.IsSuccess.Should().BeTrue();

            // Assert
            Assert.Equal(expected, result.Value.TotalInsentif, 6);
        }


        [Theory]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 1, 4500000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 2, 4500000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 3, 4500000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 4, 4500000)]

        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 2, 3000000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 3, 1500000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 4, 1000000)]

        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 1, 2250000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 2, 2250000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 3, 2250000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 4, 2250000)]

        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 2, 1500000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 3, 750000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 4, 500000)]

        ///
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 1, 2250000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 2, 2250000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 3, 2250000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 4, 2250000)]

        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 2, 1500000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 3, 750000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 4, 500000)]

        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 1, 1125000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 2, 1125000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 3, 1125000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 4, 1125000)]

        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 2, 750000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 3, 375000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 4, 250000)]
        public void S1_Combinations_Should_Calculate_Correctly(JenisJurnal jenisJurnal, bool mahasiswa, Peran peran, int jumlahCA, decimal expected)
        {
            // Arrange
            var input = new InsentifInput
            {
                JenisJurnal = jenisJurnal,
                Mahasiswa = mahasiswa,
                PeranPenulis = peran,
                SBU = S1SBU,
                JumlahCoAuthor = jumlahCA
            };

            // Act
            var result = InsentifCalculator.Hitung(input);
            result.IsSuccess.Should().BeTrue();

            // Assert
            Assert.Equal(expected, result.Value.TotalInsentif, 6);
        }

        [Theory]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 1, 3000000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 2, 3000000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 3, 3000000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 4, 3000000)]

        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 2, 2000000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 3, 1000000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 4, 666666.6666666666)]

        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 1, 1500000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 2, 1500000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 3, 1500000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 4, 1500000)]

        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 2, 1000000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 3, 500000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 4, 333333.3333333333)]

        ///
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 1, 1500000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 2, 1500000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 3, 1500000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 4, 1500000)]

        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 2, 1000000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 3, 500000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 4, 333333.3333333333)]

        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 1, 750000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 2, 750000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 3, 750000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 4, 750000)]

        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 2, 500000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 3, 250000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 4, 166666.6666666666)]
        public void S2_Combinations_Should_Calculate_Correctly(JenisJurnal jenisJurnal, bool mahasiswa, Peran peran, int jumlahCA, decimal expected)
        {
            var input = new InsentifInput
            {
                JenisJurnal = jenisJurnal,
                Mahasiswa = mahasiswa,
                PeranPenulis = peran,
                SBU = S2SBU,
                JumlahCoAuthor = jumlahCA
            };

            var result = InsentifCalculator.Hitung(input);
            result.IsSuccess.Should().BeTrue();

            Assert.Equal(expected, result.Value.TotalInsentif, 6);
        }

        [Theory]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 1, 2400000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 2, 2400000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 3, 2400000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 4, 2400000)]

        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 2, 1600000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 3, 800000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 4, 533333.3333333333)]

        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 1, 1200000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 2, 1200000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 3, 1200000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 4, 1200000)]

        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 2, 800000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 3, 400000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 4, 266666.6666666666)]

        ///
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 1, 1200000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 2, 1200000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 3, 1200000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 4, 1200000)]

        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 2, 800000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 3, 400000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 4, 266666.6666666666)]

        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 1, 600000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 2, 600000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 3, 600000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 4, 600000)]

        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 2, 400000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 3, 200000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 4, 133333.3333333333)]
        public void S3_Combinations_Should_Calculate_Correctly(JenisJurnal jenisJurnal, bool mahasiswa, Peran peran, int jumlahCA, decimal expected)
        {
            var input = new InsentifInput
            {
                JenisJurnal = jenisJurnal,
                Mahasiswa = mahasiswa,
                PeranPenulis = peran,
                SBU = S3SBU,
                JumlahCoAuthor = jumlahCA
            };

            var result = InsentifCalculator.Hitung(input);
            result.IsSuccess.Should().BeTrue();

            Assert.Equal(expected, result.Value.TotalInsentif, 6);
        }


        [Theory]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 1, 1500000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 2, 1500000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 3, 1500000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 4, 1500000)]

        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 2, 1000000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 3, 500000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 4, 333333.3333333333)]

        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 1, 750000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 2, 750000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 3, 750000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 4, 750000)]

        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 2, 500000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 3, 250000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 4, 166666.6666666666)]

        ///
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 1, 750000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 2, 750000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 3, 750000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 4, 750000)]

        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 2, 500000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 3, 250000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 4, 166666.6666666666)]

        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 1, 375000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 2, 375000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 3, 375000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 4, 375000)]

        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 2, 250000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 3, 125000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 4, 83333.3333333333)]
        public void S4_Combinations_Should_Calculate_Correctly(JenisJurnal jenisJurnal, bool mahasiswa, Peran peran, int jumlahCA, decimal expected)
        {
            var input = new InsentifInput
            {
                JenisJurnal = jenisJurnal,
                Mahasiswa = mahasiswa,
                PeranPenulis = peran,
                SBU = S4SBU,
                JumlahCoAuthor = jumlahCA
            };

            var result = InsentifCalculator.Hitung(input);
            result.IsSuccess.Should().BeTrue();

            Assert.Equal(expected, result.Value.TotalInsentif, 6);
        }


        [Theory]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 1, 450000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 2, 450000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 3, 450000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 4, 450000)]

        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 2, 300000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 3, 150000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 4, 100000)]

        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 1, 225000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 2, 225000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 3, 225000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 4, 225000)]

        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 2, 150000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 3, 75000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 4, 50000)]

        ///
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 1, 225000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 2, 225000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 3, 225000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 4, 225000)]

        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 2, 150000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 3, 75000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 4, 50000)]

        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 1, 112500)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 2, 112500)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 3, 112500)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 4, 112500)]

        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 2, 75000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 3, 37500)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 4, 25000)]
        public void S5_Combinations_Should_Calculate_Correctly(JenisJurnal jenisJurnal, bool mahasiswa, Peran peran, int jumlahCA, decimal expected)
        {
            var input = new InsentifInput
            {
                JenisJurnal = jenisJurnal,
                Mahasiswa = mahasiswa,
                PeranPenulis = peran,
                SBU = S5SBU,
                JumlahCoAuthor = jumlahCA
            };

            var result = InsentifCalculator.Hitung(input);
            result.IsSuccess.Should().BeTrue();

            Assert.Equal(expected, result.Value.TotalInsentif, 6);
        }


        [Theory]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 1, 3000000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 2, 3000000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 3, 3000000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 4, 3000000)]

        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 2, 2000000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 3, 1000000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 4, 666666.6666666666)]

        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 1, 1500000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 2, 1500000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 3, 1500000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 4, 1500000)]

        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 2, 1000000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 3, 500000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 4, 333333.3333333333)]

        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 1, 1500000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 2, 1500000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 3, 1500000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 4, 1500000)]

        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 2, 1000000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 3, 500000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 4, 333333.3333333333)]

        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 1, 750000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 2, 750000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 3, 750000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 4, 750000)]

        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 2, 500000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 3, 250000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 4, 166666.6666666666)]
        public void Internasional_Combinations_Should_Calculate_Correctly(JenisJurnal jenisJurnal, bool mahasiswa, Peran peran, int jumlahCA, decimal expected)
        {
            var input = new InsentifInput
            {
                JenisJurnal = jenisJurnal,
                Mahasiswa = mahasiswa,
                PeranPenulis = peran,
                SBU = InternasionalSBU,
                JumlahCoAuthor = jumlahCA
            };

            var result = InsentifCalculator.Hitung(input);
            result.IsSuccess.Should().BeTrue();

            Assert.Equal(expected, result.Value.TotalInsentif, 6);
        }


        [Theory]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 1, 300000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 2, 300000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 3, 300000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 4, 300000)]

        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 2, 200000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 3, 100000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 4, 66666.6666666666)]

        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 1, 150000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 2, 150000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 3, 150000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 4, 150000)]

        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 2, 100000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 3, 50000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 4, 33333.3333333333)]

        ///
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 1, 150000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 2, 150000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 3, 150000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 4, 150000)]

        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 2, 100000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 3, 50000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 4, 33333.3333333333)]

        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 1, 75000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 2, 75000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 3, 75000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 4, 75000)]

        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 2, 50000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 3, 25000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 4, 16666.6666666666)]
        public void Issn_Combinations_Should_Calculate_Correctly(JenisJurnal jenisJurnal, bool mahasiswa, Peran peran, int jumlahCA, decimal expected)
        {
            var input = new InsentifInput
            {
                JenisJurnal = jenisJurnal,
                Mahasiswa = mahasiswa,
                PeranPenulis = peran,
                SBU = IssnSBU,
                JumlahCoAuthor = jumlahCA
            };

            var result = InsentifCalculator.Hitung(input);
            result.IsSuccess.Should().BeTrue();

            Assert.Equal(expected, result.Value.TotalInsentif, 6);
        }


        [Theory]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 1, 1800000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 2, 1800000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 3, 1800000)]
        [InlineData(JenisJurnal.External, false, Peran.FirstAuthor, 4, 1800000)]

        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 2, 1200000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 3, 600000)]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 4, 400000)]

        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 1, 900000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 2, 900000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 3, 900000)]
        [InlineData(JenisJurnal.External, true, Peran.FirstAuthor, 4, 900000)]

        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 2, 600000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 3, 300000)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 4, 200000)]

        ///
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 1, 900000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 2, 900000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 3, 900000)]
        [InlineData(JenisJurnal.Internal, false, Peran.FirstAuthor, 4, 900000)]

        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 2, 600000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 3, 300000)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 4, 200000)]

        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 1, 450000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 2, 450000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 3, 450000)]
        [InlineData(JenisJurnal.Internal, true, Peran.FirstAuthor, 4, 450000)]

        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 2, 300000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 3, 150000)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 4, 100000)]
        public void Prosiding_Combinations_Should_Calculate_Correctly(JenisJurnal jenisJurnal, bool mahasiswa, Peran peran, int jumlahCA, decimal expected)
        {
            var input = new InsentifInput
            {
                JenisJurnal = jenisJurnal,
                Mahasiswa = mahasiswa,
                PeranPenulis = peran,
                SBU = ProsidingSBU,
                JumlahCoAuthor = jumlahCA
            };

            var result = InsentifCalculator.Hitung(input);
            result.IsSuccess.Should().BeTrue();

            Assert.Equal(expected, result.Value.TotalInsentif, 6);
        }


        [Theory]
        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 1, 0)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 1, 0)]

        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, -1, 0)]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, -1, 0)]
        ///
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 1, 0)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 1, 0)]

        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, -1, 0)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, -1, 0)]
        public void Combinations_Should_Fail_When_CoAuthor_Is_Alone(JenisJurnal jenisJurnal, bool mahasiswa, Peran peran, int jumlahCA, decimal expected)
        {
            // Arrange
            var input = new InsentifInput
            {
                JenisJurnal = jenisJurnal,
                Mahasiswa = mahasiswa,
                PeranPenulis = peran,
                SBU = Q1SBU,
                JumlahCoAuthor = jumlahCA
            };

            // Act
            var result = InsentifCalculator.Hitung(input);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("Insentif.InvalidDataVerifikasi");
            result.Error.Description.Should().Be("Verifikasi is invalid data");
        }

        [Theory(Skip = "JumlahCoAuthor terlalu besar, di loloskan dan dianggap 0")]
        [InlineData(JenisJurnal.External, true, Peran.CoAuthor, 2147483647)]
        [InlineData(JenisJurnal.Internal, true, Peran.CoAuthor, 2147483647)]

        [InlineData(JenisJurnal.External, false, Peran.CoAuthor, 2147483647)]
        [InlineData(JenisJurnal.Internal, false, Peran.CoAuthor, 2147483647)]
        public void Should_Not_Return_Zero(JenisJurnal jenisJurnal, bool mahasiswa, Peran peran, int jumlahCA)
        {
            var input = new InsentifInput
            {
                JenisJurnal = jenisJurnal,
                Mahasiswa = mahasiswa,
                PeranPenulis = peran,
                SBU = Q1SBU, // bisa ganti dengan Q2SBU, ProsidingSBU, dsb.
                JumlahCoAuthor = jumlahCA
            };

            var result = InsentifCalculator.Hitung(input);

            result.IsSuccess.Should().BeTrue();

            decimal insentif = result.Value.TotalInsentif;

            // Assert nilai harus finite dan > 0
            Math.Floor(insentif).Should().BeGreaterThan(0);
            double.IsNaN((double)insentif).Should().BeFalse();
            double.IsInfinity((double)insentif).Should().BeFalse();
        }


    }
}