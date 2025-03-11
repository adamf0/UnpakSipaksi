using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.CreateKualitasKuantitasPublikasiJurnalIlmiah
{
    public sealed record CreateKualitasKuantitasPublikasiJurnalIlmiahCommand(
        string Nama,
        int Nilai
    ) : ICommand<Guid>;
}
