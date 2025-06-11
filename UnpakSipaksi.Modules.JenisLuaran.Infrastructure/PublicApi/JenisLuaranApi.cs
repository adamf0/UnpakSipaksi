using MediatR;
using UnpakSipaksi.Common.Domain;

using IJenisLuaranApi = UnpakSipaksi.Modules.JenisLuaran.PublicApi.IJenisLuaranApi;
using JenisLuaranResponseApi = UnpakSipaksi.Modules.JenisLuaran.PublicApi.JenisLuaranResponse;
using UnpakSipaksi.Modules.JenisLuaran.Application.GetJenisLuaran;
using UnpakSipaksi.Modules.JenisLuaran.Application.GetJenisLuaran;

namespace UnpakSipaksi.Modules.JenisLuaran.Infrastructure.PublicApi
{
    internal sealed class JenisLuaranApi(ISender sender) : IJenisLuaranApi
    {
        public async Task<JenisLuaranResponseApi?> GetAsync(Guid JenisLuaranUuid, CancellationToken cancellationToken = default)
        {
            Result<JenisLuaranDefaultResponse> result = await sender.Send(new GetJenisLuaranDefaultQuery(JenisLuaranUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new JenisLuaranResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama
            );
        }
    }

}
