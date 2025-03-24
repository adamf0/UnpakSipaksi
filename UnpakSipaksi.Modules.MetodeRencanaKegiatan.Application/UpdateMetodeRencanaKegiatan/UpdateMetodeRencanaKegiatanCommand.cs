using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.UpdateMetodeRencanaKegiatan
{
    public sealed record UpdateMetodeRencanaKegiatanCommand(
        Guid Uuid,
        string Nama,
        int Nilai
    ) : ICommand;
}
