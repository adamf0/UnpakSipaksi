using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Application.GetRoadmapPenelitian
{
    public sealed record GetRoadmapPenelitianQuery(Guid RoadmapPenelitianUuid) : IQuery<RoadmapPenelitianResponse>;
}
