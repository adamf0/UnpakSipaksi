using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Kategori.PublicApi;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenPendukung;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateDokumenPendukung
{
    internal sealed class UpdateDokumenPendukungCommandHandler(
        IDokumenPendukungRepository dokumenPendukungRepository,
        IPenelitianHibahRepository penelitianHibahRepository,
        IUnitOfWorkDokumenPendukung unitOfWork)
        : ICommandHandler<UpdateDokumenPendukungCommand>
    {
        public async Task<Result> Handle(UpdateDokumenPendukungCommand request, CancellationToken cancellationToken)
        {
            Domain.DokumenPendukung.DokumenPendukung? existingDokumenPendukung = await dokumenPendukungRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);
            if (existingDokumenPendukung is null)
            {
                return Result.Failure(DokumenPendukungErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianHibah), cancellationToken);

            var createResult = Domain.DokumenPendukung.DokumenPendukung.Update(
                Guid.Parse(request.Uuid),
                Guid.Parse(request.UuidPenelitianHibah),
                existingPenelitianHibah,
                existingDokumenPendukung,
                request.File,
                request.Link,
                request.Kategori
            );

            if (createResult.IsFailure)
                return Result.Failure<Guid>(createResult.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(createResult.Value.Uuid);
        }
    }
}
