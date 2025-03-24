﻿using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.GetPotensiKetercapaianLuaranDijanjikan
{
    public sealed record GetPotensiKetercapaianLuaranDijanjikanQuery(Guid PotensiKetercapaianLuaranDijanjikanUuid) : IQuery<PotensiKetercapaianLuaranDijanjikanResponse>;
}
