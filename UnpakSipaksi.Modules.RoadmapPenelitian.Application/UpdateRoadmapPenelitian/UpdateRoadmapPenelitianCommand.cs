using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Application.UpdateRoadmapPenelitian
{
    public sealed record UpdateRoadmapPenelitianCommand(
        Guid Uuid,
        string Nama,
        int Skor
    ) : ICommand;
}
