using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Domain.RelevansiKualitasReferensi;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.UpdateRelevansiKualitasReferensi
{
    internal sealed class UpdateRelevansiKualitasReferensiCommandHandler(
    IRelevansiKualitasReferensiRepository RelevansiKualitasReferensiRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateRelevansiKualitasReferensiCommand>
    {
        public async Task<Result> Handle(UpdateRelevansiKualitasReferensiCommand request, CancellationToken cancellationToken)
        {
            Domain.RelevansiKualitasReferensi.RelevansiKualitasReferensi? existingRelevansiKualitasReferensi = await RelevansiKualitasReferensiRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingRelevansiKualitasReferensi is null)
            {
                Result.Failure(RelevansiKualitasReferensiErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.RelevansiKualitasReferensi.RelevansiKualitasReferensi> asset = Domain.RelevansiKualitasReferensi.RelevansiKualitasReferensi.Update(existingRelevansiKualitasReferensi!)
                         .ChangeNama(request.Nama)
                         .ChangeSkor(request.Skor)
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
