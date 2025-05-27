using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.UpdateKetajamanPerumusanMasalah
{
    public sealed record UpdateKetajamanPerumusanMasalahCommand(
        string Uuid,
        string Nama,
        int Skor
    ) : ICommand;
}
