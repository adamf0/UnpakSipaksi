using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Application.UpdateKuantitasStatusKi
{
    public sealed record UpdateKuantitasStatusKiCommand(
        Guid Uuid,
        string Nama,
        int Nilai
    ) : ICommand;
}
