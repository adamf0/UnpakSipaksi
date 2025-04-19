using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.GetKredibilitasMitraDukungan
{
    public sealed record GetKredibilitasMitraDukunganDefaultQuery(Guid KredibilitasMitraDukunganUuid) : IQuery<KredibilitasMitraDukunganDefaultResponse>;
}
