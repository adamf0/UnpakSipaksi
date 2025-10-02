using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.LaporanKemajuan.Application.Dokumen.Hibah.GetDokumenHibah;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Application.Dokumen.Hibah.GetAllDokumenHibah
{
    public sealed record GetAllDokumenHibahQuery(Guid UuidPenelitianHibah) : IQuery<List<DokumenHibahResponse>>;
}
