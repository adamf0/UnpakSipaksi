using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.GetRumusanPrioritasMitra
{
    public sealed record GetRumusanPrioritasMitraQuery(Guid RumusanPrioritasMitraUuid) : IQuery<RumusanPrioritasMitraResponse>;
}
