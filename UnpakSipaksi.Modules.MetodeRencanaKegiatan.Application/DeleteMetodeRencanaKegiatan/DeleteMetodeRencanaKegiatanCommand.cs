using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.DeleteMetodeRencanaKegiatan
{
    public sealed record DeleteMetodeRencanaKegiatanCommand(
        string uuid
    ) : ICommand;
}
