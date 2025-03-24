using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.CreateMetodeRencanaKegiatan
{
    public sealed record CreateMetodeRencanaKegiatanCommand(
        string Nama,
        int Nilai
    ) : ICommand<Guid>;
}
