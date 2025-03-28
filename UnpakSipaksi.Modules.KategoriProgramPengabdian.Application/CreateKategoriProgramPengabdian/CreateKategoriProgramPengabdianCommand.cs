using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.CreateKategoriProgramPengabdian
{
    public sealed record CreateKategoriProgramPengabdianCommand(
        string Nama,
        string Rule
    ) : ICommand<Guid>;
}
