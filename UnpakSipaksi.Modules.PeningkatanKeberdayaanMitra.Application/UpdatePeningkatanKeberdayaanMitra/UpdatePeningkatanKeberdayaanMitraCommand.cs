using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.UpdatePeningkatanKeberdayaanMitra
{
    public sealed record UpdatePeningkatanKeberdayaanMitraCommand(
        Guid Uuid,
        string Nama,
        int Nilai
    ) : ICommand;
}
