using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasIpteks.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KualitasIpteks.Domain.KualitasIpteks;

namespace UnpakSipaksi.Modules.KualitasIpteks.Application.DeleteKualitasIpteks
{
    internal sealed class DeleteKualitasIpteksCommandHandler(
    IKualitasIpteksRepository KualitasIpteksRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKualitasIpteksCommand>
    {
        public async Task<Result> Handle(DeleteKualitasIpteksCommand request, CancellationToken cancellationToken)
        {
            Domain.KualitasIpteks.KualitasIpteks? existingKualitasIpteks = await KualitasIpteksRepository.GetAsync(request.uuid, cancellationToken);

            if (existingKualitasIpteks is null)
            {
                return Result.Failure(KualitasIpteksErrors.NotFound(request.uuid));
            }

            await KualitasIpteksRepository.DeleteAsync(existingKualitasIpteks!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
