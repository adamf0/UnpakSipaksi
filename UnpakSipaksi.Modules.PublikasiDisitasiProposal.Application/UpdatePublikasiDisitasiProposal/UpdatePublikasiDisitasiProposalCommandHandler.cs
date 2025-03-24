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
            Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal? existingPublikasiDisitasiProposal = await PublikasiDisitasiProposalRepository.GetAsync(request.Uuid, cancellationToken);

            if (existingPublikasiDisitasiProposal is null)
            {
                Result.Failure(PublikasiDisitasiProposalErrors.NotFound(request.Uuid));
            }

            Result<Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal> asset = Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal.Update(existingPublikasiDisitasiProposal!)
                         .ChangeNama(request.Nama)
                         .ChangeBobotPDP(request.BobotPDP)
                         .ChangeBobotTerapan(request.BobotTerapan)
                         .ChangeBobotPenelitianDasar(request.BobotPenelitianDasar)
                         .ChangeBobotKerjasama(request.BobotKerjasama)
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
