using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Application.GetKuantitasStatusKi
{
    public sealed record GetKuantitasStatusKiQuery(Guid KuantitasStatusKiUuid) : IQuery<KuantitasStatusKiResponse>;
}
