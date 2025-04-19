using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Pengumuman.Application.UpdatePengumuman
{
    public sealed record UpdatePengumumanCommand(
        Guid Uuid,
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
    ) : ICommand;
}
