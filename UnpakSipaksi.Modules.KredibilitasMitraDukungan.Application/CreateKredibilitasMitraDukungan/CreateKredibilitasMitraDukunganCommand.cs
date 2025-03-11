using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.CreateKredibilitasMitraDukungan
{
    public sealed record CreateKredibilitasMitraDukunganCommand(
        string Nama,
        int BobotPDP,
        int BobotTerapan,
        int BobotKerjasama,
        int BobotPenelitianDasar,
        int Skor
    ) : ICommand<Guid>;
}
