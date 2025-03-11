using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Application.CreateKuantitasStatusKi
{
    public sealed record CreateKuantitasStatusKiCommand(
        string Nama,
        int Nilai
    ) : ICommand<Guid>;
}
