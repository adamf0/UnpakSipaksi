using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.Substansi;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.CreateSubstansiUsulan
{
    internal sealed class CreateSubstansiUsulanCommandHandler(
        ISubstansiRepository substansiRepository,
        IPenelitianPkmRepository penelitianHibahRepository,
        IUnitOfWorkSubstansi unitOfWork)
        : ICommandHandler<CreateSubstansiUsulanCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateSubstansiUsulanCommand request, CancellationToken cancellationToken)
        {
            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianPkm), cancellationToken);

            var result = Domain.Substansi.Substansi.Create(
                Guid.Parse(request.UuidPenelitianPkm),
                existingPenelitianPkm,
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
