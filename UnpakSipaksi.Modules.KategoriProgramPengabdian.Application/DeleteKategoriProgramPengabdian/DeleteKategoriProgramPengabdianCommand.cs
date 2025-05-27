using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.DeleteKategoriProgramPengabdian
{
    public sealed record DeleteKategoriProgramPengabdianCommand(
        string uuid
    ) : ICommand;
}
