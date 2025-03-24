using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.GetPeningkatanKeberdayaanMitra;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.GetAllPeningkatanKeberdayaanMitra
{
    public sealed record GetAllPeningkatanKeberdayaanMitraQuery() : IQuery<List<PeningkatanKeberdayaanMitraResponse>>;
}
