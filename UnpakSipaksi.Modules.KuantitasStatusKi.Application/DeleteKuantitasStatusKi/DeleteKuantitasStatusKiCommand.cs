using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Application.DeleteKuantitasStatusKi
{
    public sealed record DeleteKuantitasStatusKiCommand(
        string uuid
    ) : ICommand;
}
