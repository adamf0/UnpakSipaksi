using MediatR;
using UnpakSipaksi.Common.Domain;

using IModelFeasibilityStudysApi = UnpakSipaksi.Modules.ModelFeasibilityStudys.PublicApi.IModelFeasibilityStudysApi;
using ModelFeasibilityStudysResponseApi = UnpakSipaksi.Modules.ModelFeasibilityStudys.PublicApi.ModelFeasibilityStudysResponse;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.GetModelFeasibilityStudys;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.GetBobotModelFeasibilityStudys;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Infrastructure.PublicApi
{
    internal sealed class ModelFeasibilityStudysApi(ISender sender) : IModelFeasibilityStudysApi
    {
        public async Task<ModelFeasibilityStudysResponseApi?> GetAsync(Guid ModelFeasibilityStudysUuid, CancellationToken cancellationToken = default)
        {
            Result<ModelFeasibilityStudysDefaultResponse> result = await sender.Send(new GetModelFeasibilityStudysDefaultQuery(ModelFeasibilityStudysUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new ModelFeasibilityStudysResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama,
                result.Value.BobotPDP,
                result.Value.BobotTerapan,
                result.Value.BobotKerjasama,
                result.Value.BobotPenelitianDasar,
                result.Value.Skor
            );
        }

        public async Task<int?> GetBobotWithoutTargetAsync(string KategoriSkema, CancellationToken cancellationToken = default)
        {
            Result<int?> result = await sender.Send(new GetBobotModelFeasibilityStudysQuery(KategoriSkema), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
