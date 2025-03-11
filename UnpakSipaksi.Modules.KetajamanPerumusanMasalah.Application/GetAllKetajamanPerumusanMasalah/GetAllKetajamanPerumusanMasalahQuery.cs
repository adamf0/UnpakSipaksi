using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.GetKetajamanPerumusanMasalah;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.GetAllKetajamanPerumusanMasalah
{
    public sealed record GetAllKetajamanPerumusanMasalahQuery() : IQuery<List<KetajamanPerumusanMasalahResponse>>;
}
