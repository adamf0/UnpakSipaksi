using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.Substansi;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.CreateSubstansiUsulan
{
    internal sealed class CreateSubstansiUsulanCommandHandler(
        ISubstansiRepository substansiRepository,
        IPenelitianHibahRepository penelitianHibahRepository,
        IUnitOfWorkSubstansi unitOfWork)
        : ICommandHandler<CreateSubstansiUsulanCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateSubstansiUsulanCommand request, CancellationToken cancellationToken)
        {
            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianHibah), cancellationToken);

            var result = Domain.Substansi.Substansi.Create(
                Guid.Parse(request.UuidPenelitianHibah),
                existingPenelitianHibah,
                request.File
            );

            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);

            substansiRepository.Insert(result.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(result.Value.Uuid);
        }
    }
}
