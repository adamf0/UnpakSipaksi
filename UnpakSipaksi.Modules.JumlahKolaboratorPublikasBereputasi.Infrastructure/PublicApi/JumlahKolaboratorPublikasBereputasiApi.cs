using MediatR;
using UnpakSipaksi.Common.Domain;

using IJumlahKolaboratorPublikasBereputasiApi = UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.PublicApi.IJumlahKolaboratorPublikasBereputasiApi;
using JumlahKolaboratorPublikasBereputasiResponseApi = UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.PublicApi.JumlahKolaboratorPublikasBereputasiResponse;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.GetBobotJumlahKolaboratorPublikasBereputasi;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.GetJumlahKolaboratorPublikasBereputasi;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Infrastructure.PublicApi
{
    internal sealed class JumlahKolaboratorPublikasBereputasiApi(ISender sender) : IJumlahKolaboratorPublikasBereputasiApi
    {
        public async Task<JumlahKolaboratorPublikasBereputasiResponseApi?> GetAsync(Guid JumlahKolaboratorPublikasBereputasiUuid, CancellationToken cancellationToken = default)
        {
            Result<JumlahKolaboratorPublikasBereputasiDefaultResponse> result = await sender.Send(new GetJumlahKolaboratorPublikasBereputasiDefaultQuery(JumlahKolaboratorPublikasBereputasiUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new JumlahKolaboratorPublikasBereputasiResponseApi(
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
            Result<int?> result = await sender.Send(new GetBobotJumlahKolaboratorPublikasBereputasiQuery(KategoriSkema), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
