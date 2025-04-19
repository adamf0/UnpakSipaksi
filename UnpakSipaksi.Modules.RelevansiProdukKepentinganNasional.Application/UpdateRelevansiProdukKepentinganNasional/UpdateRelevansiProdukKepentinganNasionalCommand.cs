using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.UpdateRelevansiProdukKepentinganNasional
{
    public sealed record UpdateRelevansiProdukKepentinganNasionalCommand(
        Guid Uuid,
        string Nama,
        int Skor
    ) : ICommand;
}
