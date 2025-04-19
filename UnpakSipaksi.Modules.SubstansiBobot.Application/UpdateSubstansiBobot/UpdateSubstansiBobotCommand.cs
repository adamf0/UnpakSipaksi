using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.SubstansiBobot.Application.UpdateSubstansiBobot
{
    public sealed record UpdateSubstansiBobotCommand(
        Guid Uuid,
        string Nama,
        int Nilai
    ) : ICommand;
}
