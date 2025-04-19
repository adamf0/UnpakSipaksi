using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.Pengumuman.Domain.Pengumuman;

namespace UnpakSipaksi.Modules.Pengumuman.Application.CreatePengumuman
{
    public sealed record CreatePengumumanCommand(
        string Pesan,
        string? File,
        string? Url,
        string Type,
        string? Target,
        string? Nidn,
        string? KodeFaKultas,
        string TypeExpired,
        string? TanggalAwal,
        string? TanggalAkhir
    ) : ICommand<Guid>;
}
