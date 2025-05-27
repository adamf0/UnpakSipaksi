using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.UpdateKategoriProgramPengabdian
{
    public sealed record UpdateKategoriProgramPengabdianCommand(
        string Uuid,
        string Nama,
        string Rule
    ) : ICommand;
}
