using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.GetKategoriProgramPengabdian;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.GetAllKategoriProgramPengabdian
{
    public sealed record GetAllKategoriProgramPengabdianQuery() : IQuery<List<KategoriProgramPengabdianResponse>>;
}
