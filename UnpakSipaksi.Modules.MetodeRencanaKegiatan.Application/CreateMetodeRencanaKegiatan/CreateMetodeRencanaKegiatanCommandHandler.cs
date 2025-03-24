using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Domain.MetodeRencanaKegiatan;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.CreateMetodeRencanaKegiatan
{
    internal sealed class CreateMetodeRencanaKegiatanCommandHandler(
    IMetodeRencanaKegiatanRepository MetodeRencanaKegiatanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateMetodeRencanaKegiatanCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateMetodeRencanaKegiatanCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan> result = Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan.Create(
                request.Nama,
                request.Nilai
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            MetodeRencanaKegiatanRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
