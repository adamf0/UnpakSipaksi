using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.GetRelevansiProdukKepentinganNasional
{
    public sealed record GetRelevansiProdukKepentinganNasionalQuery(Guid Uuid) : IQuery<RelevansiProdukKepentinganNasionalResponse>;
}
