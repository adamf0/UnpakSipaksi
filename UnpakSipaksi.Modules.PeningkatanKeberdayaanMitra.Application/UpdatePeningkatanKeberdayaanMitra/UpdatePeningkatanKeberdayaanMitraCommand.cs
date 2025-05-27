using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.UpdatePeningkatanKeberdayaanMitra
{
    public sealed record UpdatePeningkatanKeberdayaanMitraCommand(
        string Uuid,
        string Nama,
        int Nilai
    ) : ICommand;
}
