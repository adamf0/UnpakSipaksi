using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.RoadmapPenelitian.Application.GetRoadmapPenelitian;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Application.GetAllRoadmapPenelitian
{
    public sealed record GetAllRoadmapPenelitianQuery() : IQuery<List<RoadmapPenelitianResponse>>;
}
