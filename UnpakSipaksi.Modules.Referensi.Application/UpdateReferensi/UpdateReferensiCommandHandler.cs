using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KebaruanReferensi.PublicApi;
using UnpakSipaksi.Modules.Referensi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Referensi.Domain.Referensi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.PublicApi;

namespace UnpakSipaksi.Modules.Referensi.Application.UpdateReferensi
{
    internal sealed class UpdateReferensiCommandHandler(
    IReferensiRepository ReferensiRepository,
    IKebaruanReferensiApi KebaruanReferensiApi,
    IRelevansiKualitasReferensiApi RelevansiKualitasReferensiApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateReferensiCommand>
    {
        public async Task<Result> Handle(UpdateReferensiCommand request, CancellationToken cancellationToken)
        {
            var kebaruanTask = KebaruanReferensiApi.GetAsync(Guid.Parse(request.UuidKebaruanReferensi));
            var relevansiTask = RelevansiKualitasReferensiApi.GetAsync(Guid.Parse(request.UuidRelevansiKualitasReferensi));

            await Task.WhenAll(kebaruanTask, relevansiTask);

            KebaruanReferensiResponse? KebaruanReferensi = await kebaruanTask;
            RelevansiKualitasReferensiResponse? RelevansiKualitasReferensi = await relevansiTask;

            Domain.Referensi.Referensi? existingReferensi = await ReferensiRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingReferensi is null)
            {
                return Result.Failure(ReferensiErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.Referensi.Referensi> asset = Domain.Referensi.Referensi.Update(existingReferensi!)
                         .ChangeNama(request.Nama)
                         .ChangeKebaruanReferensiId(int.Parse(KebaruanReferensi?.Id ?? "0"))
                         .ChangeRelevansiKualitasReferensiId(int.Parse(RelevansiKualitasReferensi?.Id ?? "0"))
                         .ChangeNilai(request.Nilai)
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
