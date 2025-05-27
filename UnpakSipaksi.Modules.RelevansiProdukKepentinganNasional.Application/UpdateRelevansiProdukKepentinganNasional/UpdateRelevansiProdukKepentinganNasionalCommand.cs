using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.UpdateRelevansiProdukKepentinganNasional
{
    public sealed record UpdateRelevansiProdukKepentinganNasionalCommand(
        string Uuid,
        string Nama,
        int Skor
    ) : ICommand;
}
