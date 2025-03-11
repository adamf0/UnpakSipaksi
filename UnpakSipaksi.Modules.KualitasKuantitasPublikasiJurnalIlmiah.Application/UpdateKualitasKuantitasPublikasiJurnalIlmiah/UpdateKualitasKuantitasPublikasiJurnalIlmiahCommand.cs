using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.UpdateKualitasKuantitasPublikasiJurnalIlmiah
{
    public sealed record UpdateKualitasKuantitasPublikasiJurnalIlmiahCommand(
        Guid Uuid,
        string Nama,
        int Nilai
    ) : ICommand;
}
