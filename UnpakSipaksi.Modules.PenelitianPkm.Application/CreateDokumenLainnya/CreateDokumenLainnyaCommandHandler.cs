using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenLainnya;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.CreateDokumenLainnya
{
    internal sealed class CreateDokumenLainnyaCommandHandler(
        IDokumenLainnyaRepository dokumenLainnyaRepository,
        IPenelitianPkmRepository penelitianPkmRepository,
        IUnitOfWorkDokumenLainnya unitOfWork)
        : ICommandHandler<CreateDokumenLainnyaCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateDokumenLainnyaCommand request, CancellationToken cancellationToken)
        {
            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm = await penelitianPkmRepository.GetAsync(Guid.Parse(request.UuidPenelitianPkm), cancellationToken);

            var result = Domain.DokumenLainnya.DokumenLainnya.Create(
                Guid.Parse(request.UuidPenelitianPkm),
                existingPenelitianPkm,
                request.File,
                request.Kategori
            );

            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);

            dokumenLainnyaRepository.Insert(result.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(result.Value.Uuid);
        }
    }
}
