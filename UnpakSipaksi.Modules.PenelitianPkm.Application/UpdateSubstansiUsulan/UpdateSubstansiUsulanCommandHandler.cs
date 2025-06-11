using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.Substansi;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateSubstansiUsulan
{
    internal sealed class UpdateSubstansiUsulanCommandHandler(
        ISubstansiRepository substansiRepository,
        IPenelitianPkmRepository penelitianHibahRepository,
        IUnitOfWorkSubstansi unitOfWork)
        : ICommandHandler<UpdateSubstansiUsulanCommand>
    {
        public async Task<Result> Handle(UpdateSubstansiUsulanCommand request, CancellationToken cancellationToken)
        {
            Domain.Substansi.Substansi? existingSubstansi = await substansiRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);
            if (existingSubstansi != null)
            {
                return Result.Failure<Guid>(SubstansiErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianPkm), cancellationToken);

            var updateResult = Domain.Substansi.Substansi.Update(
                Guid.Parse(request.Uuid),
                Guid.Parse(request.UuidPenelitianPkm),
                existingPenelitianPkm,
                existingSubstansi!,
                request.File
            );

            if (updateResult.IsFailure)
                return Result.Failure(updateResult.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
