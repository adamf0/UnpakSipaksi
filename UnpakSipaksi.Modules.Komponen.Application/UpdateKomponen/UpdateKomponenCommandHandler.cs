using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Komponen.Domain.Komponen;
using UnpakSipaksi.Modules.Komponen.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.Komponen.Application.UpdateKomponen
{
    internal sealed class UpdateKomponenCommandHandler(
    IKomponenRepository KomponenRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKomponenCommand>
    {
        public async Task<Result> Handle(UpdateKomponenCommand request, CancellationToken cancellationToken)
        {
            Domain.Komponen.Komponen? existingKomponen = await KomponenRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingKomponen is null)
            {
                return Result.Failure(KomponenErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.Komponen.Komponen> asset = Domain.Komponen.Komponen.Update(existingKomponen!)
                         .ChangeNama(request.Nama)
                         .ChangeMaxBiaya(request.MaxBiaya)
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
