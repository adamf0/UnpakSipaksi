using FluentAssertions;
using Moq;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.DomainTest.DokumenPendukung;

public class DokumenPendukungTests
{
    private async Task<Domain.PenelitianHibah.PenelitianHibah> CreateHibahAsync()
    {
        var mockRepo = new Mock<IPenelitianHibahRepository>();
        mockRepo.Setup(x =>
            x.HasUniqueDataAsync(
                It.IsAny<Guid?>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>())
        ).ReturnsAsync(true);

        var hibahResult = await Domain.PenelitianHibah.PenelitianHibah.Create(
            mockRepo.Object,
            "123",
            "2024-01-01",
            "judul"
        );

        hibahResult.IsSuccess.Should().BeTrue();
        var hibah = hibahResult.Value;

        // Set private ID via reflection
        hibah.GetType()?.GetProperty("Id")?.SetValue(hibah, 1);
        return hibah;
    }

    public static IEnumerable<object[]> ValidAndInvalidDokumenData
    {
        get
        {
            return new List<object[]>
        {
            new object?[] { null, null, "Kategori", false, "EmptyResource" },
            new object?[] { "file.pdf", "https://drive.google.com/file/d/123/view", "Kategori", false, "DuplicateResource" },
            new object?[] { null, "https://example.com", "Kategori", false, "InvalidLink" },
            new object?[] { "file.pdf", null, "Kategori", true, null },
            new object?[] { null, "https://drive.google.com/file/d/abc123/view", "Kategori", true, null },
        };
        }
    }

    [Theory]
    [MemberData(nameof(ValidAndInvalidDokumenData))]
    public async Task Create_ShouldValidateCorrectly_WithHibah(
        string? file,
        string? link,
        string kategori,
        bool expectedSuccess,
        string? expectedErrorKey)
    {
        // Arrange
        var hibah = await CreateHibahAsync();

        // Act
        var result = Domain.DokumenPendukung.DokumenPendukung.Create(Guid.NewGuid(), hibah, file, link, kategori);

        // Assert
        result.IsSuccess.Should().Be(expectedSuccess);

        if (expectedSuccess)
        {
            result.Value.File.Should().Be(file);
            result.Value.Link.Should().Be(link);
            result.Value.Kategori.Should().Be(kategori);
        }
        else
        {
            result.IsFailure.Should().BeTrue();
            result.Error.ToString().Should().Contain(expectedErrorKey);
        }
    }

    public static IEnumerable<object[]> DokumenWithNullHibahData =>
        new List<object[]>
        {
            new object?[] { null, null, "EmptyResource" },
            new object?[] { "file.pdf", "https://drive.google.com/file/d/123/view", "DuplicateResource" },
            new object?[] { null, "https://example.com", "InvalidLink" },
            new object?[] { "file.pdf", null, "NotFoundHibah" },
            new object?[] { null, "https://drive.google.com/file/d/abc123/view", "NotFoundHibah" },
        };

    [Theory]
    [MemberData(nameof(DokumenWithNullHibahData))]
    public async Task Create_ShouldFail_WhenHibahIsNull(
        string? file,
        string? link,
        string expectedErrorKey)
    {
        // Arrange
        Domain.PenelitianHibah.PenelitianHibah? hibah = null;

        if (expectedErrorKey != "NotFoundHibah")
        {
            hibah = await CreateHibahAsync();
        }

        // Act
        var result = Domain.DokumenPendukung.DokumenPendukung.Create(Guid.NewGuid(), hibah, file, link, "Kategori");

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.ToString().Should().Contain(expectedErrorKey);
    }
}
