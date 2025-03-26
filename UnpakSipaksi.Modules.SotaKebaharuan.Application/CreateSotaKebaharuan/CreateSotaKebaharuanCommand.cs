using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Application.CreateSotaKebaharuan
{
    public sealed record CreateSotaKebaharuanCommand(
        string Nama,
        int BobotPDP,
        int BobotTerapan,
        int BobotKerjasama,
        int BobotPenelitianDasar,
        int Skor
    ) : ICommand<Guid>;
}
