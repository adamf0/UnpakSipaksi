using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Kategori.PublicApi;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenKontrak;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateDokumenKontrak
{
    internal sealed class UpdateDokumenKontrakCommandHandler(
        IDokumenKontrakRepository dokumenKontrakRepository,
        IPenelitianHibahRepository penelitianHibahRepository,
        IUnitOfWorkDokumenKontrak unitOfWork)
        : ICommandHandler<UpdateDokumenKontrakCommand>
    {
        public async Task<Result> Handle(UpdateDokumenKontrakCommand request, CancellationToken cancellationToken)
        {
            Domain.DokumenKontrak.DokumenKontrak? existingDokumenKontrak = await dokumenKontrakRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);
            if (existingDokumenKontrak is null)
            {
                return Result.Failure(DokumenKontrakErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianHibah), cancellationToken);

            var createResult = Domain.DokumenKontrak.DokumenKontrak.Update(
                Guid.Parse(request.Uuid),
                Guid.Parse(request.UuidPenelitianHibah),
                existingPenelitianHibah,
                existingDokumenKontrak,
                request.File
            );

            if (createResult.IsFailure)
                return Result.Failure<Guid>(createResult.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(createResult.Value.Uuid);
        }
    }
}
