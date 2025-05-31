using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenKontrak;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.CreateDokumenKontrak
{
    internal sealed class CreateDokumenKontrakCommandHandler(
        IDokumenKontrakRepository dokumenPendukungRepository,
        IPenelitianHibahRepository penelitianHibahRepository,
        IUnitOfWorkDokumenKontrak unitOfWork)
        : ICommandHandler<CreateDokumenKontrakCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateDokumenKontrakCommand request, CancellationToken cancellationToken)
        {
            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianHibah), cancellationToken);

            var result = Domain.DokumenKontrak.DokumenKontrak.Create(
                Guid.Parse(request.UuidPenelitianHibah),
                existingPenelitianHibah,
                request.File
            );

            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);

            dokumenPendukungRepository.Insert(result.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(result.Value.Uuid);
        }
    }
}
