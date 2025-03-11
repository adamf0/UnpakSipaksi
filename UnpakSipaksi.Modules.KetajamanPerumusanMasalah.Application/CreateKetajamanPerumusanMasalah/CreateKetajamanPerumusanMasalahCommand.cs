using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.CreateKetajamanPerumusanMasalah
{
    public sealed record CreateKetajamanPerumusanMasalahCommand(
        string Nama,
        int BobotPDP,
        int BobotTerapan,
        int BobotKerjasama,
        int BobotPenelitianDasar,
        int Skor
    ) : ICommand<Guid>;
}
