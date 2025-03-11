using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.GetKredibilitasMitraDukungan;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.GetAllKredibilitasMitraDukungan
{
    public sealed record GetAllKredibilitasMitraDukunganQuery() : IQuery<List<KredibilitasMitraDukunganResponse>>;
}
