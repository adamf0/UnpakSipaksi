using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.GetRelevansiProdukKepentinganNasional
{
    public sealed record GetRelevansiProdukKepentinganNasionalDefaultQuery(Guid Uuid) : IQuery<RelevansiProdukKepentinganNasionalDefaultResponse>;
}
