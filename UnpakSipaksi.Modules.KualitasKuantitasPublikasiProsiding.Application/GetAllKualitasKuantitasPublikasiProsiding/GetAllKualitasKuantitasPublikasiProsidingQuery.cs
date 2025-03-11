﻿using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.GetKualitasKuantitasPublikasiProsiding;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.GetAllKualitasKuantitasPublikasiProsiding
{
    public sealed record GetAllKualitasKuantitasPublikasiProsidingQuery() : IQuery<List<KualitasKuantitasPublikasiProsidingResponse>>;
}
