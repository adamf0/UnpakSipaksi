using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Domain.KewajaranTahapanTarget;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.DeleteKewajaranTahapanTarget
{
    internal sealed class DeleteKewajaranTahapanTargetCommandHandler(
    IKewajaranTahapanTargetRepository KewajaranTahapanTargetRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKewajaranTahapanTargetCommand>
    {
        public async Task<Result> Handle(DeleteKewajaranTahapanTargetCommand request, CancellationToken cancellationToken)
        {
            Domain.KewajaranTahapanTarget.KewajaranTahapanTarget? existingKewajaranTahapanTarget = await KewajaranTahapanTargetRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingKewajaranTahapanTarget is null)
            {
                return Result.Failure(KewajaranTahapanTargetErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await KewajaranTahapanTargetRepository.DeleteAsync(existingKewajaranTahapanTarget!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
