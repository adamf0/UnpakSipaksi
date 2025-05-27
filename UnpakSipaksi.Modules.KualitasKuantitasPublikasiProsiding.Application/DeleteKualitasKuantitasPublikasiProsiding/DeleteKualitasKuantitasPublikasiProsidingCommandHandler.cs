using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Domain.KualitasKuantitasPublikasiProsiding;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.DeleteKualitasKuantitasPublikasiProsiding
{
    internal sealed class DeleteKualitasKuantitasPublikasiProsidingCommandHandler(
    IKualitasKuantitasPublikasiProsidingRepository KualitasKuantitasPublikasiProsidingRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKualitasKuantitasPublikasiProsidingCommand>
    {
        public async Task<Result> Handle(DeleteKualitasKuantitasPublikasiProsidingCommand request, CancellationToken cancellationToken)
        {
            Domain.KualitasKuantitasPublikasiProsiding.KualitasKuantitasPublikasiProsiding? existingKualitasKuantitasPublikasiProsiding = await KualitasKuantitasPublikasiProsidingRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingKualitasKuantitasPublikasiProsiding is null)
            {
                return Result.Failure(KualitasKuantitasPublikasiProsidingErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await KualitasKuantitasPublikasiProsidingRepository.DeleteAsync(existingKualitasKuantitasPublikasiProsiding!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
