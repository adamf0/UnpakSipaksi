using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.UpdateMetodeRencanaKegiatan
{
    public sealed record UpdateMetodeRencanaKegiatanCommand(
        string Uuid,
        string Nama,
        int Nilai
    ) : ICommand;
}
