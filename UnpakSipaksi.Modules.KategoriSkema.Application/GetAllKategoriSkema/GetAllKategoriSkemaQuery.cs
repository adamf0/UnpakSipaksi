using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KategoriSkema.Application.GetKategoriSkema;

namespace UnpakSipaksi.Modules.KategoriSkema.Application.GetAllKategoriSkema
{
    public sealed record GetAllKategoriSkemaQuery() : IQuery<List<KategoriSkemaResponse>>;
}
