using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.DeleteModelFeasibilityStudys
{
    public sealed record DeleteModelFeasibilityStudysCommand(
        string uuid
    ) : ICommand;
}
