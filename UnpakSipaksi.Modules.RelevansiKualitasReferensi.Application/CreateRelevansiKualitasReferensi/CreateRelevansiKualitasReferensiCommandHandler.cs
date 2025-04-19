using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Domain.RelevansiKualitasReferensi;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.CreateRelevansiKualitasReferensi
{
    internal sealed class CreateRelevansiKualitasReferensiCommandHandler(
    IRelevansiKualitasReferensiRepository RelevansiKualitasReferensiRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRelevansiKualitasReferensiCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateRelevansiKualitasReferensiCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.RelevansiKualitasReferensi.RelevansiKualitasReferensi> result = Domain.RelevansiKualitasReferensi.RelevansiKualitasReferensi.Create(
                request.Nama,
                request.Skor
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            RelevansiKualitasReferensiRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
