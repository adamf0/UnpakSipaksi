using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.UpdateKualitasKuantitasPublikasiJurnalIlmiah
{
    public sealed record UpdateKualitasKuantitasPublikasiJurnalIlmiahCommand(
        string Uuid,
        string Nama,
        int Nilai
    ) : ICommand;
}
