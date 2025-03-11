using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Application.GetKetajamanAnalisis
{
    public sealed record GetKetajamanAnalisisQuery(Guid KetajamanAnalisisUuid) : IQuery<KetajamanAnalisisResponse>;
}
