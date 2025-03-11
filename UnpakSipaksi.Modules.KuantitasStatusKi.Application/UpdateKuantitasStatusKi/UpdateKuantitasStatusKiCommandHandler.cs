using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KuantitasStatusKi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KuantitasStatusKi.Domain.KuantitasStatusKi;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Application.UpdateKuantitasStatusKi
{
    internal sealed class UpdateKuantitasStatusKiCommandHandler(
    IKuantitasStatusKiRepository KuantitasStatusKiRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKuantitasStatusKiCommand>
    {
        public async Task<Result> Handle(UpdateKuantitasStatusKiCommand request, CancellationToken cancellationToken)
        {
            Domain.KuantitasStatusKi.KuantitasStatusKi? existingKuantitasStatusKi = await KuantitasStatusKiRepository.GetAsync(request.Uuid, cancellationToken);

            if (existingKuantitasStatusKi is null)
            {
                Result.Failure(KuantitasStatusKiErrors.NotFound(request.Uuid));
            }

            Result<Domain.KuantitasStatusKi.KuantitasStatusKi> asset = Domain.KuantitasStatusKi.KuantitasStatusKi.Update(existingKuantitasStatusKi!)
                         .ChangeNama(request.Nama)
                         .ChangeNilai(request.Nilai)
                         .Build();

            if (asset.IsFailure)
            {
                return Result.Failure<Guid>(asset.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
