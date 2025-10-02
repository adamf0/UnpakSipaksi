using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.LaporanKemajuan.Application.Luaran.Hibah.GetLuaranHibah;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Application.Luaran.Hibah.GetLuaranHibah
{
    public sealed record GetLuaranHibahQuery(Guid Uuid, Guid LuaranUuid) : IQuery<LuaranHibahResponse>;
}
