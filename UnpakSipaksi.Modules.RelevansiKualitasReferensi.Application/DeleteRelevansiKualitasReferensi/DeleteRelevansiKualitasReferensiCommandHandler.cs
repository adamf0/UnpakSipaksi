using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.Domain.RelevansiKualitasReferensi;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.DeleteRelevansiKualitasReferensi
{
    internal sealed class DeleteRelevansiKualitasReferensiCommandHandler(
    IRelevansiKualitasReferensiRepository RelevansiKualitasReferensiRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteRelevansiKualitasReferensiCommand>
    {
        public async Task<Result> Handle(DeleteRelevansiKualitasReferensiCommand request, CancellationToken cancellationToken)
        {
            Domain.RelevansiKualitasReferensi.RelevansiKualitasReferensi? existingRelevansiKualitasReferensi = await RelevansiKualitasReferensiRepository.GetAsync(request.uuid, cancellationToken);

            if (existingRelevansiKualitasReferensi is null)
            {
                return Result.Failure(RelevansiKualitasReferensiErrors.NotFound(request.uuid));
            }

            await RelevansiKualitasReferensiRepository.DeleteAsync(existingRelevansiKualitasReferensi!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
