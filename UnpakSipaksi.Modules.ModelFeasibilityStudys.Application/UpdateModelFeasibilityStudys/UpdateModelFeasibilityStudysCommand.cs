using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.UpdateModelFeasibilityStudys
{
    public sealed record UpdateModelFeasibilityStudysCommand(
        Guid Uuid,
        string Nama,
        int BobotPDP,
        int BobotTerapan,
        int BobotKerjasama,
        int BobotPenelitianDasar,
        int Skor
    ) : ICommand;
}
