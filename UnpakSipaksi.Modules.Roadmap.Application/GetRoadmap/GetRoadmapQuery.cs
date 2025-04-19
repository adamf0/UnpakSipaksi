using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Roadmap.Application.GetRoadmap
{
    public sealed record GetRoadmapQuery(Guid RoadmapUuid) : IQuery<RoadmapResponse>;
}
