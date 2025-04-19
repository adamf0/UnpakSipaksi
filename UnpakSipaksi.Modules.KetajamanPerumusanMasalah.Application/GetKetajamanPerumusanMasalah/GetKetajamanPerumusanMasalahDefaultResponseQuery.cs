using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.GetKetajamanPerumusanMasalah
{
    public sealed record GetKetajamanPerumusanMasalahDefaultQuery(Guid KetajamanPerumusanMasalahUuid) : IQuery<KetajamanPerumusanMasalahDefaultResponse>;
}
