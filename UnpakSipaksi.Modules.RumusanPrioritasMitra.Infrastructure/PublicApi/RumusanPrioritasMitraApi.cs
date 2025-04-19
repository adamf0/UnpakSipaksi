using MediatR;
using UnpakSipaksi.Common.Domain;

using IRumusanPrioritasMitraApi = UnpakSipaksi.Modules.RumusanPrioritasMitra.PublicApi.IRumusanPrioritasMitraApi;
using RumusanPrioritasMitraResponseApi = UnpakSipaksi.Modules.RumusanPrioritasMitra.PublicApi.RumusanPrioritasMitraResponse;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.GetRumusanPrioritasMitra;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.GetBobotRumusanPrioritasMitra;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Infrastructure.PublicApi
{
    internal sealed class RumusanPrioritasMitraApi(ISender sender) : IRumusanPrioritasMitraApi
    {
        public async Task<RumusanPrioritasMitraResponseApi?> GetAsync(Guid RumusanPrioritasMitraUuid, CancellationToken cancellationToken = default)
        {
            Result<RumusanPrioritasMitraDefaultResponse> result = await sender.Send(new GetRumusanPrioritasMitraDefaultQuery(RumusanPrioritasMitraUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new RumusanPrioritasMitraResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama,
                result.Value.Nilai
            );
        }

        public async Task<int?> GetBobotWithoutTargetAsync(CancellationToken cancellationToken = default)
        {
            Result<int?> result = await sender.Send(new GetBobotRumusanPrioritasMitraQuery(), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
