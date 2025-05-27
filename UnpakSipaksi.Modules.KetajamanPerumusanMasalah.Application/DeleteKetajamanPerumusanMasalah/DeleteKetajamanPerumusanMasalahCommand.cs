using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.DeleteKetajamanPerumusanMasalah
{
    public sealed record DeleteKetajamanPerumusanMasalahCommand(
        string uuid
    ) : ICommand;
}
