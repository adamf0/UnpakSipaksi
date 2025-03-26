using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriSkema.Application.UpdateKategoriSkema
{
    public sealed record UpdateKategoriSkemaCommand(
        Guid Uuid,
        string Nama,
        string Rule
    ) : ICommand;
}
