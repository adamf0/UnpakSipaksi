using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Application.DeleteRoadmapPenelitian
{
    public sealed record DeleteRoadmapPenelitianCommand(
        string uuid
    ) : ICommand;
}
