using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Domain.RelevansiKepakaranTemaProposal;

namespace UnpakSipaksi.Modules.RelevansiKepakaranTemaProposal.Application.CreateRelevansiKepakaranTemaProposal
{
    internal sealed class CreateRelevansiKepakaranTemaProposalCommandHandler(
    IRelevansiKepakaranTemaProposalRepository RelevansiKepakaranTemaProposalRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRelevansiKepakaranTemaProposalCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateRelevansiKepakaranTemaProposalCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal> result = Domain.RelevansiKepakaranTemaProposal.RelevansiKepakaranTemaProposal.Create(
                request.Nama,
                request.Skor
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            RelevansiKepakaranTemaProposalRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
