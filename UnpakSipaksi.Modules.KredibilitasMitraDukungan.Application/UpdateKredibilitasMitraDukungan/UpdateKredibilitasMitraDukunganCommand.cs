using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.UpdateKredibilitasMitraDukungan
{
    public sealed record UpdateKredibilitasMitraDukunganCommand(
        Guid Uuid,
        string Nama,
        int Skor
    ) : ICommand;
}
