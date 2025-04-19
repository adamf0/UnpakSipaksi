using MediatR;
using UnpakSipaksi.Common.Domain;

using IKredibilitasMitraDukunganApi = UnpakSipaksi.Modules.KredibilitasMitraDukungan.PublicApi.IKredibilitasMitraDukunganApi;
using KredibilitasMitraDukunganResponseApi = UnpakSipaksi.Modules.KredibilitasMitraDukungan.PublicApi.KredibilitasMitraDukunganResponse;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.GetKredibilitasMitraDukungan;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.GetBobotKredibilitasMitraDukungan;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Infrastructure.PublicApi
{
    internal sealed class KredibilitasMitraDukunganApi(ISender sender) : IKredibilitasMitraDukunganApi
    {
        public async Task<KredibilitasMitraDukunganResponseApi?> GetAsync(Guid KredibilitasMitraDukunganUuid, CancellationToken cancellationToken = default)
        {
            Result<KredibilitasMitraDukunganDefaultResponse> result = await sender.Send(new GetKredibilitasMitraDukunganDefaultQuery(KredibilitasMitraDukunganUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new KredibilitasMitraDukunganResponseApi(
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
            Result<int?> result = await sender.Send(new GetBobotKredibilitasMitraDukunganQuery(KategoriSkema), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
