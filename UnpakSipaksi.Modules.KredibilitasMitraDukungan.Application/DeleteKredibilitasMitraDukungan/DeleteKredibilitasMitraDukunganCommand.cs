using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.DeleteKredibilitasMitraDukungan
{
    public sealed record DeleteKredibilitasMitraDukunganCommand(
        Guid uuid
    ) : ICommand;
}
