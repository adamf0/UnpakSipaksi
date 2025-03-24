using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.PublikasiDisitasiProposal;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Domain.PublikasiDisitasiProposal;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.DeletePublikasiDisitasiProposal
{
    internal sealed class DeletePublikasiDisitasiProposalCommandHandler(
    IPublikasiDisitasiProposalRepository PublikasiDisitasiProposalRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeletePublikasiDisitasiProposalCommand>
    {
        public async Task<Result> Handle(DeletePublikasiDisitasiProposalCommand request, CancellationToken cancellationToken)
        {
            Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal? existingPublikasiDisitasiProposal = await PublikasiDisitasiProposalRepository.GetAsync(request.uuid, cancellationToken);

            if (existingPublikasiDisitasiProposal is null)
            {
                return Result.Failure(PublikasiDisitasiProposalErrors.NotFound(request.uuid));
            }

            await PublikasiDisitasiProposalRepository.DeleteAsync(existingPublikasiDisitasiProposal!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
