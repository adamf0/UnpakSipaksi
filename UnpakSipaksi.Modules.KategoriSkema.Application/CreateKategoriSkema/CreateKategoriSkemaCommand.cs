using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriSkema.Application.CreateKategoriSkema
{
    public sealed record CreateKategoriSkemaCommand(
        string Nama,
        string Rule
    ) : ICommand<Guid>;
}
