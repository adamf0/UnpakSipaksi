﻿using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.GetPotensiKetercapaianLuaranDijanjikan;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.GetAllPotensiKetercapaianLuaranDijanjikan
{
    public sealed record GetAllPotensiKetercapaianLuaranDijanjikanQuery() : IQuery<List<PotensiKetercapaianLuaranDijanjikanResponse>>;
}
