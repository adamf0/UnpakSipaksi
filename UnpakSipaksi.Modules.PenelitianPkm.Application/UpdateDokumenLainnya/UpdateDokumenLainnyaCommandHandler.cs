using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenLainnya;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenLainnya;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateDokumenLainnya
{
    internal sealed class UpdateDokumenLainnyaCommandHandler(
        IDokumenLainnyaRepository dokumenLainnyaRepository,
        IPenelitianPkmRepository penelitianPkmRepository,
        IUnitOfWorkDokumenLainnya unitOfWork)
        : ICommandHandler<UpdateDokumenLainnyaCommand>
    {
        public async Task<Result> Handle(UpdateDokumenLainnyaCommand request, CancellationToken cancellationToken)
        {
            Domain.DokumenLainnya.DokumenLainnya? existingDokumenLainnya = await dokumenLainnyaRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);
            if (existingDokumenLainnya is null)
            {
                return Result.Failure(DokumenLainnyaErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm = await penelitianPkmRepository.GetAsync(Guid.Parse(request.UuidPenelitianPkm), cancellationToken);

            var createResult = Domain.DokumenLainnya.DokumenLainnya.Update(
                Guid.Parse(request.Uuid),
                Guid.Parse(request.UuidPenelitianPkm),
                existingPenelitianPkm,
                existingDokumenLainnya,
                request.File,
                request.Kategori
            );

            if (createResult.IsFailure)
                return Result.Failure<Guid>(createResult.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(createResult.Value.Uuid);
        }
    }
}
