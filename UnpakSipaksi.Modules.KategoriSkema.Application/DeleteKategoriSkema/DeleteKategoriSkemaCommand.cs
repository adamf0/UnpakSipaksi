using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriSkema.Application.DeleteKategoriSkema
{
    public sealed record DeleteKategoriSkemaCommand(
        Guid uuid
    ) : ICommand;
}
