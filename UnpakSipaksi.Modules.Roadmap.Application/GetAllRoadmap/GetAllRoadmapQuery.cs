using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.Roadmap.Application.GetRoadmap;

namespace UnpakSipaksi.Modules.Roadmap.Application.GetAllRoadmap
{
    public sealed record GetAllRoadmapQuery() : IQuery<List<RoadmapResponse>>;
}
