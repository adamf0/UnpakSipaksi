using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Domain.RelevansiKepakaranTemaProposal;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.DeleteRelevansiKepakaranTemaProposal
{
    internal sealed class DeleteRelevansiKepakaranTemaProposalCommandHandler(
    IRelevansiKepakaranTemaProposalRepository RelevansiKepakaranTemaProposalRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteRelevansiKepakaranTemaProposalCommand>
    {
        public async Task<Result> Handle(DeleteRelevansiKepakaranTemaProposalCommand request, CancellationToken cancellationToken)
        {
            Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal? existingRelevansiKepakaranTemaProposal = await RelevansiKepakaranTemaProposalRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingRelevansiKepakaranTemaProposal is null)
            {
                return Result.Failure(RelevansiKepakaranTemaProposalErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await RelevansiKepakaranTemaProposalRepository.DeleteAsync(existingRelevansiKepakaranTemaProposal!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
