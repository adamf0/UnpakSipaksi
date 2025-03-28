using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.UpdateKategoriProgramPengabdian
{
    public sealed record UpdateKategoriProgramPengabdianCommand(
        Guid Uuid,
        string Nama,
        string Rule
    ) : ICommand;
}
