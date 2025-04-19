using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Application.CreateRoadmapPenelitian
{
    public sealed record CreateRoadmapPenelitianCommand(
        string Nama,
        int Skor
    ) : ICommand<Guid>;
}
