using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.CreateRelevansiKualitasReferensi
{
    public sealed record CreateRelevansiKualitasReferensiCommand(
        string Nama,
        int BobotPDP,
        int BobotTerapan,
        int BobotKerjasama,
        int BobotPenelitianDasar,
        int Skor
    ) : ICommand<Guid>;
}
