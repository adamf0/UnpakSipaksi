using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriSkema.Application.GetKategoriSkema
{
    public sealed record GetKategoriSkemaQuery(Guid KategoriSkemaUuid) : IQuery<KategoriSkemaResponse>;
}
