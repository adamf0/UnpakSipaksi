using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.UpdateRelevansiKualitasReferensi
{
    public sealed record UpdateRelevansiKualitasReferensiCommand(
        Guid Uuid,
        string Nama,
        int BobotPDP,
        int BobotTerapan,
        int BobotKerjasama,
        int BobotPenelitianDasar,
        int Skor
    ) : ICommand;
}
