using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.UpdateModelFeasibilityStudys
{
    public sealed record UpdateModelFeasibilityStudysCommand(
        Guid Uuid,
        string Nama,
        int Skor
    ) : ICommand;
}
