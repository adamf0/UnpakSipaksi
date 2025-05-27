using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.DeleteRelevansiProdukKepentinganNasional
{
    public sealed record DeleteRelevansiProdukKepentinganNasionalCommand(
        string uuid
    ) : ICommand;
}
