﻿using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.GetKualitasKuantitasPublikasiProsiding
{
    public sealed record GetKualitasKuantitasPublikasiProsidingQuery(Guid KualitasKuantitasPublikasiProsidingUuid) : IQuery<KualitasKuantitasPublikasiProsidingResponse>;
}
