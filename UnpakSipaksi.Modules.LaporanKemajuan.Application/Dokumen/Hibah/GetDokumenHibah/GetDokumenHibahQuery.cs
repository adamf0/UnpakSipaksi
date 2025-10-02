using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Application.Dokumen.Hibah.GetDokumenHibah
{
    public sealed record GetDokumenHibahQuery(Guid Uuid, Guid DokumenHibahUuid) : IQuery<DokumenHibahResponse>;
}
