using FluentAssertions;
using Moq;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;


namespace UnpakSipaksi.Modules.PenelitianPkm.DomainTest.DokumenLainnya;

public class DokumenLainnyaTests
{
    private static Mock<IPenelitianPkmRepository> CreateMockRepo(bool hasUniqueData = true)
    {
        var mock = new Mock<IPenelitianPkmRepository>();
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

    private async Task<Domain.PenelitianPkm.PenelitianPkm> CreateValidHibah(int id = 1)
    {
        var repo = CreateMockRepo();
        var result = await Domain.PenelitianPkm.PenelitianPkm.Create(repo.Object, "123", "2024-01-01", "judul");
        result.IsSuccess.Should().BeTrue();

        var hibah = result.Value;
        SetId(hibah, id);
        return hibah;
    }

    private Domain.DokumenLainnya.DokumenLainnya CreateValidDokumen(Domain.PenelitianPkm.PenelitianPkm hibah, string file = "file.pdf", string keterangan = "keterangan")
    {
        var result = Domain.DokumenLainnya.DokumenLainnya.Create(Guid.NewGuid(), hibah, file, keterangan);
        result.IsSuccess.Should().BeTrue();
        var kontrak = result.Value;
        SetId(kontrak, 1);
        typeof(Domain.DokumenLainnya.DokumenLainnya)
            .GetProperty("PenelitianPkmId")?
            .SetValue(kontrak, 1);
        return kontrak;
    }

    [Fact]
    public async Task Create_ShouldSucceed_WithValidInput()
    {
        var hibah = await CreateValidHibah();
        var file = "file.pdf";
        var keterangan = "keterangan";

        var result = Domain.DokumenLainnya.DokumenLainnya.Create(Guid.NewGuid(), hibah, file, keterangan);

        result.IsSuccess.Should().BeTrue();
        result.Value.File.Should().Be(file);
        result.Value.Keterangan.Should().Be(keterangan);
    }

    [Fact]
    public async Task Update_ShouldSucceed_WithValidInput()
    {
        var hibah = await CreateValidHibah();
        var kontrak = CreateValidDokumen(hibah, "file.pdf");
        var keterangan = "";

        var updatedResult = Domain.DokumenLainnya.DokumenLainnya.Update(
            Guid.NewGuid(),
            Guid.NewGuid(),
            hibah,
            kontrak,
            "file2.pdf",
            keterangan
        );

        updatedResult.IsSuccess.Should().BeTrue();
        updatedResult.Value.File.Should().Be("file2.pdf");
        updatedResult.Value.Keterangan.Should().Be(keterangan);
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
        Domain.PenelitianPkm.PenelitianPkm? hibah = hibahState == "valid"
            ? await CreateValidHibah()
            : null;

        var result = Domain.DokumenLainnya.DokumenLainnya.Create(Guid.NewGuid(), hibah, file, "keterangan");

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
        Domain.PenelitianPkm.PenelitianPkm? hibah = null;
        Domain.DokumenLainnya.DokumenLainnya? prev = null;

        if (hibahState is "valid" or "mismatch")
        {
            int hibahId = hibahState == "valid" ? 1 : 2;
            hibah = await CreateValidHibah(hibahId);
        }

        if (hasPrev && hibah != null)
        {
            prev = CreateValidDokumen(hibah);
        }

        var result = Domain.DokumenLainnya.DokumenLainnya.Update(
            Guid.NewGuid(),
            Guid.NewGuid(),
            hibah,
            prev,
            newFile,
            "keterangan"
        );

        result.IsSuccess.Should().Be(expectedSuccess);
    }
}