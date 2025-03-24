﻿using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.GetRelevansiProdukKepentinganNasional;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.GetAllRelevansiProdukKepentinganNasional
{
    public sealed record GetAllRelevansiProdukKepentinganNasionalQuery() : IQuery<List<RelevansiProdukKepentinganNasionalResponse>>;
}
