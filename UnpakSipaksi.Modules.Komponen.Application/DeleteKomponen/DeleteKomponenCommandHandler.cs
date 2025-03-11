using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Komponen.Domain.Komponen;
using UnpakSipaksi.Modules.Komponen.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.Komponen.Application.DeleteKomponen
{
    internal sealed class DeleteKomponenCommandHandler(
    IKomponenRepository KomponenRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKomponenCommand>
    {
        public async Task<Result> Handle(DeleteKomponenCommand request, CancellationToken cancellationToken)
        {
            Domain.Komponen.Komponen? existingKomponen = await KomponenRepository.GetAsync(request.uuid, cancellationToken);

            if (existingKomponen is null)
            {
                return Result.Failure(KomponenErrors.NotFound(request.uuid));
            }

            await KomponenRepository.DeleteAsync(existingKomponen!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
