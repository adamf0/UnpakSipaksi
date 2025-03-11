using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KuantitasStatusKi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KuantitasStatusKi.Domain.KuantitasStatusKi;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Application.CreateKuantitasStatusKi
{
    internal sealed class CreateKuantitasStatusKiCommandHandler(
    IKuantitasStatusKiRepository KuantitasStatusKiRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKuantitasStatusKiCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKuantitasStatusKiCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KuantitasStatusKi.KuantitasStatusKi> result = Domain.KuantitasStatusKi.KuantitasStatusKi.Create(
                request.Nama,
                request.Nilai
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            KuantitasStatusKiRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
