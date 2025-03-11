using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.GetKetajamanPerumusanMasalah
{
    public sealed record GetKetajamanPerumusanMasalahQuery(Guid KetajamanPerumusanMasalahUuid) : IQuery<KetajamanPerumusanMasalahResponse>;
}
