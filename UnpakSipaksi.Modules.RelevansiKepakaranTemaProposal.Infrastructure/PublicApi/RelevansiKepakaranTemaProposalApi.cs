using MediatR;
using UnpakSipaksi.Common.Domain;

using IRelevansiKepakaranTemaProposalApi = UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.PublicApi.IRelevansiKepakaranTemaProposalApi;
using RelevansiKepakaranTemaProposalResponseApi = UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.PublicApi.RelevansiKepakaranTemaProposalResponse;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.GetRelevansiKepakaranTemaProposal;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.GetBobotRelevansiKepakaranTemaProposal;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Infrastructure.PublicApi
{
    internal sealed class RelevansiKepakaranTemaProposalApi(ISender sender) : IRelevansiKepakaranTemaProposalApi
    {
        public async Task<RelevansiKepakaranTemaProposalResponseApi?> GetAsync(Guid RelevansiKepakaranTemaProposalUuid, CancellationToken cancellationToken = default)
        {
            Result<RelevansiKepakaranTemaProposalDefaultResponse> result = await sender.Send(new GetRelevansiKepakaranTemaProposalDefaultQuery(RelevansiKepakaranTemaProposalUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new RelevansiKepakaranTemaProposalResponseApi(
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
            Result<int?> result = await sender.Send(new GetBobotRelevansiKepakaranTemaProposalQuery(KategoriSkema), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return result.Value;
        }
    }

}
