using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.CreateRelevansiProdukKepentinganNasional
{
    public sealed record CreateRelevansiProdukKepentinganNasionalCommand(
        string Nama,
        int BobotPDP,
        int BobotTerapan,
        int BobotKerjasama,
        int BobotPenelitianDasar,
        int Skor
    ) : ICommand<Guid>;
}
