using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Application.CreateRoadmapPenelitian
{
    public sealed record CreateRoadmapPenelitianCommand(
        string Nama,
        int BobotPDP,
        int BobotTerapan,
        int BobotKerjasama,
        int BobotPenelitianDasar,
        int Skor
    ) : ICommand<Guid>;
}
