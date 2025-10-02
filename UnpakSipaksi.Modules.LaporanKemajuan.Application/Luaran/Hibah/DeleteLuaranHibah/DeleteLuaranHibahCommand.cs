using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Application.Luaran.Hibah.DeleteLuaranHibah
{
    public sealed record DeleteLuaranHibahCommand(
        string uuid
    ) : ICommand;
}
