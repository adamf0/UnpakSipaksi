using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KuantitasStatusKi.Application.GetKuantitasStatusKi;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Application.GetAllKuantitasStatusKi
{
    public sealed record GetAllKuantitasStatusKiQuery() : IQuery<List<KuantitasStatusKiResponse>>;
}
