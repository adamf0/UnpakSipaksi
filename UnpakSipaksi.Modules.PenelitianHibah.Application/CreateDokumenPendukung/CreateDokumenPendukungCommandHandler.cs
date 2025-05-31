using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenPendukung;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.CreateDokumenPendukung
{
    internal sealed class CreateDokumenPendukungCommandHandler(
        IDokumenPendukungRepository dokumenPendukungRepository,
        IPenelitianHibahRepository penelitianHibahRepository,
        IUnitOfWorkDokumenPendukung unitOfWork)
        : ICommandHandler<CreateDokumenPendukungCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateDokumenPendukungCommand request, CancellationToken cancellationToken)
        {
            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianHibah), cancellationToken);

            var result = Domain.DokumenPendukung.DokumenPendukung.Create(
                Guid.Parse(request.UuidPenelitianHibah),
                existingPenelitianHibah,
                request.File,
                request.Link,
                request.Kategori
            );

            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);

            dokumenPendukungRepository.Insert(result.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(result.Value.Uuid);
        }
    }
}
