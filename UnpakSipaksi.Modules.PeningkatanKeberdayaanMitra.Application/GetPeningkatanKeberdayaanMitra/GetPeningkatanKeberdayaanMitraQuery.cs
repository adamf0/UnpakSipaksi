using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.GetPeningkatanKeberdayaanMitra
{
    public sealed record GetPeningkatanKeberdayaanMitraQuery(Guid PeningkatanKeberdayaanMitraUuid) : IQuery<PeningkatanKeberdayaanMitraResponse>;
}
