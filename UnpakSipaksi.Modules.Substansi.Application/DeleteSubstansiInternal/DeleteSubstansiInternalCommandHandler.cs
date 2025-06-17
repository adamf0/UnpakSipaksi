using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Substansi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Substansi.Domain.SubstansiInternal;

namespace UnpakSipaksi.Modules.Substansi.Application.DeleteSubstansiInternal
{
    internal sealed class DeleteSubstansiInternalCommandHandler(
    ISubstansiInternalRepository SubstansiRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteSubstansiInternalCommand>
    {
        public async Task<Result> Handle(DeleteSubstansiInternalCommand request, CancellationToken cancellationToken)
        {
            SubstansiInternal? existingSubstansi = await SubstansiRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingSubstansi is null)
            {
                return Result.Failure(SubstansiInternalErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            await SubstansiRepository.DeleteAsync(existingSubstansi!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
