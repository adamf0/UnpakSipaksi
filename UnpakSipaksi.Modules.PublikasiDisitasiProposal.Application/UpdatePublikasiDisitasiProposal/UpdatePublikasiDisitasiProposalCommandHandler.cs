using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Domain.PublikasiDisitasiProposal;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.UpdatePublikasiDisitasiProposal
{
    internal sealed class UpdatePublikasiDisitasiProposalCommandHandler(
    IPublikasiDisitasiProposalRepository PublikasiDisitasiProposalRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdatePublikasiDisitasiProposalCommand>
    {
        public async Task<Result> Handle(UpdatePublikasiDisitasiProposalCommand request, CancellationToken cancellationToken)
        {
            Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal? existingPublikasiDisitasiProposal = await PublikasiDisitasiProposalRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingPublikasiDisitasiProposal is null)
            {
                return Result.Failure(PublikasiDisitasiProposalErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal> asset = Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal.Update(existingPublikasiDisitasiProposal!)
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
