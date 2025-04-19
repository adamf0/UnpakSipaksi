using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.CreateModelFeasibilityStudys
{
    public sealed record CreateModelFeasibilityStudysCommand(
        string Nama,
        int Skor
    ) : ICommand<Guid>;
}
