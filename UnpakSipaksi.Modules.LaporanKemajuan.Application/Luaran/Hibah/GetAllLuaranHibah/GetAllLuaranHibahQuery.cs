using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.LaporanKemajuan.Application.Luaran.Hibah.GetLuaranHibah;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Application.Luaran.Hibah.GetAllDokumenHibah
{
    public sealed record GetAllLuaranHibahQuery(Guid UuidPenelitianHibah) : IQuery<List<LuaranHibahResponse>>;
}
