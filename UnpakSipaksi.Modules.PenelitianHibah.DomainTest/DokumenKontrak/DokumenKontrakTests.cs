using FluentAssertions;
using Moq;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;


namespace UnpakSipaksi.Modules.PenelitianHibah.DomainTest.DokumenKontrak;

public class DokumenKontrakTests
{
    private static Mock<IPenelitianHibahRepository> CreateMockRepo(bool hasUniqueData = true)
    {
        var mock = new Mock<IPenelitianHibahRepository>();
        mock.Setup(x => x.HasUniqueDataAsync(
            It.IsAny<Guid?>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(hasUniqueData);
        return mock;
    }

    private static void SetId<T>(T obj, int id)
    {
        typeof(T).GetProperty("Id")?.SetValue(obj, id);
    }

    private async Task<Domain.PenelitianHibah.PenelitianHibah> CreateValidHibah(int id = 1)
    {
        var repo = CreateMockRepo();
        var result = await Domain.PenelitianHibah.PenelitianHibah.Create(repo.Object, "123", "2024-01-01", "judul");
        result.IsSuccess.Should().BeTrue();

        var hibah = result.Value;
        SetId(hibah, id);
        return hibah;
    }

    private Domain.DokumenKontrak.DokumenKontrak CreateValidKontrak(Domain.PenelitianHibah.PenelitianHibah hibah, string file = "file.pdf")
    {
        var result = Domain.DokumenKontrak.DokumenKontrak.Create(Guid.NewGuid(), hibah, file);
        result.IsSuccess.Should().BeTrue();
        var kontrak = result.Value;
        SetId(kontrak, 1);
        typeof(Domain.DokumenKontrak.DokumenKontrak)
            .GetProperty("PenelitianHibahId")?
            .SetValue(kontrak, 1);
        return kontrak;
    }

    [Fact]
    public async Task Create_ShouldSucceed_WithValidInput()
    {
        var hibah = await CreateValidHibah();
        var file = "file.pdf";

        var result = Domain.DokumenKontrak.DokumenKontrak.Create(Guid.NewGuid(), hibah, file);

        result.IsSuccess.Should().BeTrue();
        result.Value.File.Should().Be(file);
    }

    [Fact]
    public async Task Update_ShouldSucceed_WithValidInput()
    {
        var hibah = await CreateValidHibah();
        var kontrak = CreateValidKontrak(hibah, "file.pdf");

        var updatedResult = Domain.DokumenKontrak.DokumenKontrak.Update(
            Guid.NewGuid(),
            Guid.NewGuid(),
            hibah,
            kontrak,
            "file2.pdf"
        );

        updatedResult.IsSuccess.Should().BeTrue();
        updatedResult.Value.File.Should().Be("file2.pdf");
    }

    public static IEnumerable<object[]> InvalidCreateData =>
        new List<object[]>
        {
            new object[] { null, "file.pdf", false },
            new object[] { "valid", null, false },
            new object[] { "valid", "", false },
        };

    [Theory]
    [MemberData(nameof(InvalidCreateData))]
    public async Task Create_ShouldFail_WithInvalidInput(string hibahState, string? file, bool expectedSuccess)
    {
        Domain.PenelitianHibah.PenelitianHibah? hibah = hibahState == "valid"
            ? await CreateValidHibah()
            : null;

        var result = Domain.DokumenKontrak.DokumenKontrak.Create(Guid.NewGuid(), hibah, file);

        result.IsSuccess.Should().Be(expectedSuccess);
    }

    public static IEnumerable<object[]> InvalidUpdateData =>
        new List<object[]>
        {
            new object[] { null, true, "file2.pdf", false },      // prev null
            new object[] { "valid", false, "file2.pdf", false },  // prev missing
            new object[] { "mismatch", true, "file2.pdf", false },// mismatched hibah ID
            new object[] { "valid", true, "", false },            // file kosong
            new object[] { "valid", true, null, false },          // file null
        };

    [Theory]
    [MemberData(nameof(InvalidUpdateData))]
    public async Task Update_ShouldFail_WithInvalidInput(
        string hibahState,
        bool hasPrev,
        string? newFile,
        bool expectedSuccess)
    {
        Domain.PenelitianHibah.PenelitianHibah? hibah = null;
        Domain.DokumenKontrak.DokumenKontrak? prev = null;

        if (hibahState is "valid" or "mismatch")
        {
            int hibahId = hibahState == "valid" ? 1 : 2;
            hibah = await CreateValidHibah(hibahId);
        }

        if (hasPrev && hibah != null)
        {
            prev = CreateValidKontrak(hibah);
        }

        var result = Domain.DokumenKontrak.DokumenKontrak.Update(
            Guid.NewGuid(),
            Guid.NewGuid(),
            hibah,
            prev,
            newFile
        );

        result.IsSuccess.Should().Be(expectedSuccess);
    }
}