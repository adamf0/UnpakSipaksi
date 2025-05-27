using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Komponen.Application.DeleteKomponen
{
    public sealed record DeleteKomponenCommand(
        string uuid
    ) : ICommand;
}
