using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Application.GetRoadmapPenelitian
{
    public sealed record GetRoadmapPenelitianDefaultQuery(Guid RoadmapPenelitianUuid) : IQuery<RoadmapPenelitianDefaultResponse>;
}
