using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.UpdateKetajamanPerumusanMasalah
{
    public sealed record UpdateKetajamanPerumusanMasalahCommand(
        Guid Uuid,
        string Nama,
        int BobotPDP,
        int BobotTerapan,
        int BobotKerjasama,
        int BobotPenelitianDasar,
        int Skor
    ) : ICommand;
}
