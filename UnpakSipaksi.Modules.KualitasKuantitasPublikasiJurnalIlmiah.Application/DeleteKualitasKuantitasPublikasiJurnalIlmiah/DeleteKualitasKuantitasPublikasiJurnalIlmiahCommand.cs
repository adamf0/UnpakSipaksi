using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.DeleteKualitasKuantitasPublikasiJurnalIlmiah
{
    public sealed record DeleteKualitasKuantitasPublikasiJurnalIlmiahCommand(
        string uuid
    ) : ICommand;
}
