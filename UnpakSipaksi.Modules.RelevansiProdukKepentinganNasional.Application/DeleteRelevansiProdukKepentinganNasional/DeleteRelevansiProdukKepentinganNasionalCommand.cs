using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.DeleteRelevansiProdukKepentinganNasional
{
    public sealed record DeleteRelevansiProdukKepentinganNasionalCommand(
        Guid uuid
    ) : ICommand;
}
