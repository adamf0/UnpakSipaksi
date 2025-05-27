using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriSkema.Application.UpdateKategoriSkema
{
    public sealed record UpdateKategoriSkemaCommand(
        string Uuid,
        string Nama,
        string Rule
    ) : ICommand;
}
