using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PublikasiDisitasiProposal.Domain.PublikasiDisitasiProposal;

namespace UnpakSipaksi.Modules.PublikasiDisitasiProposal.Application.CreatePublikasiDisitasiProposal
{
    internal sealed class CreatePublikasiDisitasiProposalCommandHandler(
    IPublikasiDisitasiProposalRepository PublikasiDisitasiProposalRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePublikasiDisitasiProposalCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreatePublikasiDisitasiProposalCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal> result = Domain.PublikasiDisitasiProposal.PublikasiDisitasiProposal.Create(
                request.Nama,
                request.Skor
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            PublikasiDisitasiProposalRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
