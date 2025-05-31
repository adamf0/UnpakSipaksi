using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Domain.RelevansiKepakaranTemaProposal;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.UpdateRelevansiKepakaranTemaProposal
{
    internal sealed class UpdateRelevansiKepakaranTemaProposalCommandHandler(
    IRelevansiKepakaranTemaProposalRepository RelevansiKepakaranTemaProposalRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateRelevansiKepakaranTemaProposalCommand>
    {
        public async Task<Result> Handle(UpdateRelevansiKepakaranTemaProposalCommand request, CancellationToken cancellationToken)
        {
            Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal? existingRelevansiKepakaranTemaProposal = await RelevansiKepakaranTemaProposalRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingRelevansiKepakaranTemaProposal is null)
            {
                return Result.Failure(RelevansiKepakaranTemaProposalErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal> asset = Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal.Update(existingRelevansiKepakaranTemaProposal!)
                         .ChangeNama(request.Nama)
                         .ChangeSkor(request.Skor)
                         .Build();

            if (asset.IsFailure)
            {
                return Result.Failure<Guid>(asset.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
