using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KuantitasStatusKi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KuantitasStatusKi.Domain.KuantitasStatusKi;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Application.DeleteKuantitasStatusKi
{
    internal sealed class DeleteKuantitasStatusKiCommandHandler(
    IKuantitasStatusKiRepository KuantitasStatusKiRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKuantitasStatusKiCommand>
    {
        public async Task<Result> Handle(DeleteKuantitasStatusKiCommand request, CancellationToken cancellationToken)
        {
            Domain.KuantitasStatusKi.KuantitasStatusKi? existingKuantitasStatusKi = await KuantitasStatusKiRepository.GetAsync(request.uuid, cancellationToken);

            if (existingKuantitasStatusKi is null)
            {
                return Result.Failure(KuantitasStatusKiErrors.NotFound(request.uuid));
            }

            await KuantitasStatusKiRepository.DeleteAsync(existingKuantitasStatusKi!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
