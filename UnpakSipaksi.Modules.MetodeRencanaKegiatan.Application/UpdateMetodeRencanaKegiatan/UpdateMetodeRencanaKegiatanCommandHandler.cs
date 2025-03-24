using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Domain.MetodeRencanaKegiatan;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.UpdateMetodeRencanaKegiatan
{
    internal sealed class UpdateMetodeRencanaKegiatanCommandHandler(
    IMetodeRencanaKegiatanRepository MetodeRencanaKegiatanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateMetodeRencanaKegiatanCommand>
    {
        public async Task<Result> Handle(UpdateMetodeRencanaKegiatanCommand request, CancellationToken cancellationToken)
        {
            Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan? existingMetodeRencanaKegiatan = await MetodeRencanaKegiatanRepository.GetAsync(request.Uuid, cancellationToken);

            if (existingMetodeRencanaKegiatan is null)
            {
                Result.Failure(MetodeRencanaKegiatanErrors.NotFound(request.Uuid));
            }

            Result<Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan> asset = Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan.Update(existingMetodeRencanaKegiatan!)
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
