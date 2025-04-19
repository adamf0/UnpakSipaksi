using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KebaruanReferensi.PublicApi;
using UnpakSipaksi.Modules.Referensi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Referensi.Application.CreateAkurasiPenelitian;
using UnpakSipaksi.Modules.Referensi.Domain.Referensi;
using UnpakSipaksi.Modules.RelevansiKualitasReferensi.PublicApi;

namespace UnpakSipaksi.Modules.Referensi.Application.CreateReferensi
{
    internal sealed class CreateReferensiCommandHandler(
    IReferensiRepository ReferensiRepository,
    IKebaruanReferensiApi KebaruanReferensiApi,
    IRelevansiKualitasReferensiApi RelevansiKualitasReferensiApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateReferensiCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateReferensiCommand request, CancellationToken cancellationToken)
        {
            var kebaruanTask = KebaruanReferensiApi.GetAsync(Guid.Parse(request.UuidKebaruanReferensi));
            var relevansiTask = RelevansiKualitasReferensiApi.GetAsync(Guid.Parse(request.UuidRelevansiKualitasReferensi));

            await Task.WhenAll(kebaruanTask, relevansiTask);

            KebaruanReferensiResponse? KebaruanReferensi = await kebaruanTask;
            RelevansiKualitasReferensiResponse? RelevansiKualitasReferensi = await relevansiTask;

            Result<Domain.Referensi.Referensi> result = Domain.Referensi.Referensi.Create(
                request.Nama,
                int.Parse(KebaruanReferensi?.Id ?? "0"),
                int.Parse(RelevansiKualitasReferensi?.Id ?? "0"),
                request.Nilai
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            ReferensiRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
