using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Application.GetKualitasKuantitasPublikasiJurnalIlmiah
{
    public sealed record GetKualitasKuantitasPublikasiJurnalIlmiahQuery(Guid KualitasIpteksUuid) : IQuery<KualitasKuantitasPublikasiJurnalIlmiahResponse>;
}
