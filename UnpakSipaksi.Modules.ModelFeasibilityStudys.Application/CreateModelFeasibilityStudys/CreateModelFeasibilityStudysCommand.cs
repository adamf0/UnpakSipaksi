using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.CreateModelFeasibilityStudys
{
    public sealed record CreateModelFeasibilityStudysCommand(
        string Nama,
        int BobotPDP,
        int BobotTerapan,
        int BobotKerjasama,
        int BobotPenelitianDasar,
        int Skor
    ) : ICommand<Guid>;
}
