using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.GetKategoriProgramPengabdian
{
    public sealed record GetKategoriProgramPengabdianQuery(Guid KategoriProgramPengabdianUuid) : IQuery<KategoriProgramPengabdianResponse>;
}
