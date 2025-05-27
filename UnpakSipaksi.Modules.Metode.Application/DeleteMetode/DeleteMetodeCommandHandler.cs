using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Metode.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Metode.Domain.Metode;

namespace UnpakSipaksi.Modules.Metode.Application.DeleteMetode
{
    internal sealed class DeleteMetodeCommandHandler(
    IMetodeRepository MetodeRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteMetodeCommand>
    {
        public async Task<Result> Handle(DeleteMetodeCommand request, CancellationToken cancellationToken)
        {
            Domain.Metode.Metode? existingMetode = await MetodeRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingMetode is null)
            {
                return Result.Failure(MetodeErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await MetodeRepository.DeleteAsync(existingMetode!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
