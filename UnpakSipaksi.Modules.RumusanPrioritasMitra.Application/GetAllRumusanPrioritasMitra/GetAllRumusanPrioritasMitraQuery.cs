using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.GetRumusanPrioritasMitra;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.GetAllRumusanPrioritasMitra
{
    public sealed record GetAllRumusanPrioritasMitraQuery() : IQuery<List<RumusanPrioritasMitraResponse>>;
}
