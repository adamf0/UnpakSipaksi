using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.GetModelFeasibilityStudys
{
    public sealed record GetModelFeasibilityStudysQuery(Guid ModelFeasibilityStudysUuid) : IQuery<ModelFeasibilityStudysResponse>;
}
