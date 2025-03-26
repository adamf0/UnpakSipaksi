using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.UpdateRumusanPrioritasMitra
{
    public sealed record UpdateRumusanPrioritasMitraCommand(
        Guid Uuid,
        string Nama,
        int Nilai
    ) : ICommand;
}
