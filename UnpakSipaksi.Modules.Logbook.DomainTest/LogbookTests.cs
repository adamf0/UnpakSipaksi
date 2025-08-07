using System;
using FluentAssertions;
using UnpakSipaksi.Modules.Logbook.Domain.Logbook;
using Xunit;

namespace UnpakSipaksi.Modules.Logbook.DomainTest
{
    public class LogbookTests
    {
        [Theory]
        [InlineData(1, 0, "2024-01-01", "lampiran.pdf", "deskripsi", 40.0, 50.0)]
        [InlineData(0, 2, "2025-08-07", "file.docx", "aktivitas", 20.0, 10.0)]
        public void Create_WhenValidPropertiesProvided_ShouldReturnLogbookWithCorrectProperties(
            int hibahId,
            int pkmId,
            string tanggal,
            string lampiran,
            string deskripsi,
            double persentase,
            double currentTotalPersentase
        )
        {
            // Act
            var result = Domain.Logbook.Logbook.Create(
                hibahId,
                pkmId,
                tanggal,
                lampiran,
                deskripsi,
                persentase,
                currentTotalPersentase
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.PenelitianHibahId.Should().Be(hibahId);
            result.Value.PenelitianPkmId.Should().Be(pkmId);
            result.Value.Deskripsi.Should().Be(deskripsi);
            result.Value.Lampiran.Should().Be(lampiran);
            result.Value.Persentase.Should().Be(persentase);
            result.Value.TanggalKegiatan.Should().Be(DateOnly.Parse(tanggal));
            result.Value.Uuid.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public void Create_WhenValid_ShouldRaiseKelompokMitraCreatedDomainEvent()
        {
            // Arrange
            var result = Domain.Logbook.Logbook.Create(1, 0, "2024-01-01", "lampiran", "deskripsi", 10.0, 50.0);

            // Assert
            result.Value.DomainEvents.Should().Contain(e => e is KelompokMitraCreatedDomainEvent);
        }

        [Theory]
        [InlineData("invalid-date", 1, 0, 10.0, 0, "Logbook.InvalidFormatDate")]
        [InlineData("2024-01-01", 1, 0, -1.0, 0, "Logbook.InvalidFormatPercentage")]
        [InlineData("2024-01-01", 1, 0, 60.0, 50.0, "Logbook.OverCapacity")]
        [InlineData("2024-01-01", 0, 0, 10.0, 0, "Logbook.NoTargetHibah")]
        [InlineData("2024-01-01", 1, 1, 10.0, 0, "Logbook.NoTargetHibah")]
        public void Create_WhenInvalidPropertiesProvided_ShouldReturnExpectedError(
            string tanggal,
            int hibahId,
            int pkmId,
            double persentase,
            double currentTotalPersentase,
            string expectedErrorCode
        )
        {
            // Act
            var result = Domain.Logbook.Logbook.Create(
                hibahId,
                pkmId,
                tanggal,
                "lampiran",
                "deskripsi",
                persentase,
                currentTotalPersentase
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(expectedErrorCode);
        }

        [Theory]
        [InlineData(1, 0, "2024-01-02", "lampiran_updated.pdf", "deskripsi_updated", 30.0, 40.0)]
        [InlineData(0, 2, "2025-08-07", "doc.pdf", "update", 15.0, 10.0)]
        public void Update_WhenValidPropertiesProvided_ShouldReturnUpdatedLogbook(
            int hibahId,
            int pkmId,
            string tanggal,
            string lampiran,
            string deskripsi,
            double persentase,
            double currentTotalPersentase
        )
        {
            // Arrange
            var prev = Domain.Logbook.Logbook.Create(hibahId, pkmId, "2024-01-01", "lampiran", "deskripsi", 10.0, 0).Value;

            // Act
            var result = Domain.Logbook.Logbook.Update(
                prev,
                hibahId,
                pkmId,
                tanggal,
                lampiran,
                deskripsi,
                persentase,
                currentTotalPersentase
            );

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Deskripsi.Should().Be(deskripsi);
            result.Value.Lampiran.Should().Be(lampiran);
            result.Value.TanggalKegiatan.Should().Be(DateOnly.Parse(tanggal));
        }

        [Theory]
        [InlineData(null, 1, 0, 10.0, 0, "Logbook.InvalidFormatDate")]
        [InlineData("2024-01-01", 1, 0, -5.0, 0, "Logbook.InvalidFormatPercentage")]
        [InlineData("2024-01-01", 1, 0, 60.0, 50.0, "Logbook.OverCapacity")]
        [InlineData("2024-01-01", 0, 0, 10.0, 0, "Logbook.NoTargetHibah")]
        [InlineData("2024-01-01", 1, 1, 10.0, 0, "Logbook.NoTargetHibah")]
        public void Update_WhenInvalidStaticPropertiesProvided_ShouldReturnExpectedError(
            string tanggal,
            int hibahId,
            int pkmId,
            double persentase,
            double currentTotalPersentase,
            string expectedErrorCode
        )
        {
            // Arrange
            var prev = Domain.Logbook.Logbook.Create(1, 0, "2024-01-01", "lampiran", "deskripsi", 10.0, 0).Value;

            // Act
            var result = Domain.Logbook.Logbook.Update(
                prev,
                hibahId,
                pkmId,
                tanggal ?? "invalid",
                "lampiran",
                "deskripsi",
                persentase,
                currentTotalPersentase
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be(expectedErrorCode);
        }

        [Fact]
        public void Update_WhenPreviousLogbookIsNull_ShouldReturnEmptyDataError()
        {
            // Act
            var result = Domain.Logbook.Logbook.Update(
                null,
                1,
                0,
                "2024-01-01",
                "lampiran",
                "deskripsi",
                10.0,
                0
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("Logbook.EmptyData");
        }

        [Fact]
        public void Update_WhenPenelitianIdsDoNotMatchPrevious_ShouldReturnInvalidDataError()
        {
            // Arrange
            var prev = Domain.Logbook.Logbook.Create(1, 0, "2024-01-01", "lampiran", "deskripsi", 10.0, 0).Value;

            // Act
            var result = Domain.Logbook.Logbook.Update(
                prev,
                99,
                0,
                "2024-01-02",
                "lampiran",
                "deskripsi",
                10.0,
                0
            );

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Code.Should().Be("Logbook.InvalidData");
        }
    }
}
