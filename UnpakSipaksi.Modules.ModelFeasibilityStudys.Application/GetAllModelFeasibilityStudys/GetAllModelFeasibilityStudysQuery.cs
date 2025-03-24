using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.GetModelFeasibilityStudys;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.GetAllModelFeasibilityStudys
{
    public sealed record GetAllModelFeasibilityStudysQuery() : IQuery<List<ModelFeasibilityStudysResponse>>;
}
