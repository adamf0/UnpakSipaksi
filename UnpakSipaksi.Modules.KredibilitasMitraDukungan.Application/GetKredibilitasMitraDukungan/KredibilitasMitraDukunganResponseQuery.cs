using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.GetKredibilitasMitraDukungan
{
    public sealed record GetKredibilitasMitraDukunganQuery(Guid KredibilitasMitraDukunganUuid) : IQuery<KredibilitasMitraDukunganResponse>;
}
