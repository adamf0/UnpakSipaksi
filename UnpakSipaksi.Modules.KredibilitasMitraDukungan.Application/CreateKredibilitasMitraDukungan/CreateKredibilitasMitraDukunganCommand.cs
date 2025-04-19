using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.CreateKredibilitasMitraDukungan
{
    public sealed record CreateKredibilitasMitraDukunganCommand(
        string Nama,
        int Skor
    ) : ICommand<Guid>;
}
