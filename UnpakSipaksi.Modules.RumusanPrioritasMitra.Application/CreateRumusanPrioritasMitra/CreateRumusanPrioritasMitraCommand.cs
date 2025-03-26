using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.CreateRumusanPrioritasMitra
{
    public sealed record CreateRumusanPrioritasMitraCommand(
        string Nama,
        int Nilai
    ) : ICommand<Guid>;
}
