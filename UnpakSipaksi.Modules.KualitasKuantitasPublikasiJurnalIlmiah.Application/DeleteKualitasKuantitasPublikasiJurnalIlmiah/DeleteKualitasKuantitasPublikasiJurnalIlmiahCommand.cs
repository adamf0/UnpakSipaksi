using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.DeleteKualitasKuantitasPublikasiJurnalIlmiah
{
    public sealed record DeleteKualitasKuantitasPublikasiJurnalIlmiahCommand(
        Guid uuid
    ) : ICommand;
}
