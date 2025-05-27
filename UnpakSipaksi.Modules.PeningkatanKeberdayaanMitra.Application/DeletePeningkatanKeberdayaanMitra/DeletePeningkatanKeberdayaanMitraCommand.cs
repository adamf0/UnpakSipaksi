using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.DeletePeningkatanKeberdayaanMitra
{
    public sealed record DeletePeningkatanKeberdayaanMitraCommand(
        string uuid
    ) : ICommand;
}
