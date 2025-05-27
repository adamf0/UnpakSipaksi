using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.DeleteRumusanPrioritasMitra
{
    public sealed record DeleteRumusanPrioritasMitraCommand(
        string uuid
    ) : ICommand;
}
