using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.CreatePeningkatanKeberdayaanMitra
{
    public sealed record CreatePeningkatanKeberdayaanMitraCommand(
        string Nama,
        int Nilai
    ) : ICommand<Guid>;
}
