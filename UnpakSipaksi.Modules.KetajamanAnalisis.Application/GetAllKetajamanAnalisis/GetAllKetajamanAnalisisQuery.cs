using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KetajamanAnalisis.Application.GetKetajamanAnalisis;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Application.GetAllKetajamanAnalisis
{
    public sealed record GetAllKetajamanAnalisisQuery() : IQuery<List<KetajamanAnalisisResponse>>;
}
