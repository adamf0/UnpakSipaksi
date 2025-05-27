using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.Substansi;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateSubstansiUsulan
{
    internal sealed class UpdateSubstansiUsulanCommandHandler(
        ISubstansiRepository substansiRepository,
        IPenelitianHibahRepository penelitianHibahRepository,
        IUnitOfWorkSubstansi unitOfWork)
        : ICommandHandler<UpdateSubstansiUsulanCommand>
    {
        public async Task<Result> Handle(UpdateSubstansiUsulanCommand request, CancellationToken cancellationToken)
        {
            Domain.Substansi.Substansi? existingSubstansi = await substansiRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);
            if (existingSubstansi != null) {
                return Result.Failure<Guid>(SubstansiErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianHibah), cancellationToken);

            var updateResult = Domain.Substansi.Substansi.Update(
                Guid.Parse(request.Uuid),
                Guid.Parse(request.UuidPenelitianHibah),
                existingPenelitianHibah,
                existingSubstansi!,
                request.File
            );

            if (updateResult.IsFailure)
                return Result.Failure<Guid>(updateResult.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
