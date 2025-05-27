using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.UpdateRumusanPrioritasMitra
{
    public sealed record UpdateRumusanPrioritasMitraCommand(
        string Uuid,
        string Nama,
        int Nilai
    ) : ICommand;
}
