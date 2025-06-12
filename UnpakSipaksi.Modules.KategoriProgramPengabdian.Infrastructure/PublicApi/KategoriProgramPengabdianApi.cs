using MediatR;
using UnpakSipaksi.Common.Domain;

using IKategoriProgramPengabdianApi = UnpakSipaksi.Modules.KategoriProgramPengabdian.PublicApi.IKategoriProgramPengabdianApi;
using KategoriProgramPengabdianResponseApi = UnpakSipaksi.Modules.KategoriProgramPengabdian.PublicApi.KategoriProgramPengabdianResponse;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.GetKategoriProgramPengabdian;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Infrastructure.PublicApi
{
    internal sealed class KategoriProgramPengabdianApi(ISender sender) : IKategoriProgramPengabdianApi
    {
        public async Task<KategoriProgramPengabdianResponseApi?> GetAsync(Guid KategoriProgramPengabdianUuid, CancellationToken cancellationToken = default)
        {
            Result<KategoriProgramPengabdianDefaultResponse> result = await sender.Send(new GetKategoriProgramPengabdianDefaultQuery(KategoriProgramPengabdianUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KategoriProgramPengabdianResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama
            );
        }
    }

}
