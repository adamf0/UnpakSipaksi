using MediatR;
using UnpakSipaksi.Common.Domain;

using IPublikasiDisitasiProposalApi = UnpakSipaksi.Modules.PublikasiDisitasiProposal.PublicApi.IPublikasiDisitasiProposalApi;
using PublikasiDisitasiProposalResponseApi = UnpakSipaksi.Modules.PublikasiDisitasiProposal.PublicApi.PublikasiDisitasiProposalResponse;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.GetBobotPublikasiDisitasiProposal;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.GetPublikasiDisitasiProposal;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Infrastructure.PublicApi
{
    internal sealed class PublikasiDisitasiProposalApi(ISender sender) : IPublikasiDisitasiProposalApi
    {
        public async Task<PublikasiDisitasiProposalResponseApi?> GetAsync(Guid PublikasiDisitasiProposalUuid, CancellationToken cancellationToken = default)
        {
            Result<PublikasiDisitasiProposalDefaultResponse> result = await sender.Send(new GetPublikasiDisitasiProposalDefaultQuery(PublikasiDisitasiProposalUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new PublikasiDisitasiProposalResponseApi(
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
            Result<int?> result = await sender.Send(new GetBobotPublikasiDisitasiProposalQuery(KategoriSkema), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
