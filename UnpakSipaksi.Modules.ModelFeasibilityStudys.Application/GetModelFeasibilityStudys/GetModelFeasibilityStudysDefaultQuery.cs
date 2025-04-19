using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.GetModelFeasibilityStudys
{
    public sealed record GetModelFeasibilityStudysDefaultQuery(Guid ModelFeasibilityStudysUuid) : IQuery<ModelFeasibilityStudysDefaultResponse>;
}
