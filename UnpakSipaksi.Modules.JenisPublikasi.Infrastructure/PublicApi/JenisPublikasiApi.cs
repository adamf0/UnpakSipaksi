using MediatR;
using UnpakSipaksi.Common.Domain;

using IJenisPublikasiApi = UnpakSipaksi.Modules.JenisPublikasi.PublicApi.IJenisPublikasiApi;
using JenisPublikasiResponseApi = UnpakSipaksi.Modules.JenisPublikasi.PublicApi.JenisPublikasiResponse;
using UnpakSipaksi.Modules.JenisPublikasi.Application.GetJenisPublikasi;

namespace UnpakSipaksi.Modules.JenisPublikasi.Infrastructure.PublicApi
{
    internal sealed class JenisPublikasiApi(ISender sender) : IJenisPublikasiApi
    {
        public async Task<JenisPublikasiResponseApi?> GetAsync(Guid JenisPublikasiUuid, CancellationToken cancellationToken = default)
        {
            Result<JenisPublikasiDefaultResponse> result = await sender.Send(new GetJenisPublikasiDefaultQuery(JenisPublikasiUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new JenisPublikasiResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.Nama,
                result.Value.Sbu
            );
        }
    }

}
