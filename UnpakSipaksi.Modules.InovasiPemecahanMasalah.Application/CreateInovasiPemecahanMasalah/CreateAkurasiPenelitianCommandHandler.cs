using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Domain.InovasiPemecahanMasalah;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.CreateInovasiPemecahanMasalah
{
    internal sealed class CreateInovasiPemecahanMasalahCommandHandler(
    IInovasiPemecahanMasalahRepository inovasiPemecahanMasalahRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateInovasiPemecahanMasalahCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateInovasiPemecahanMasalahCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah> result = Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah.Create(
                request.Nama,
                request.BobotPDP,
                request.BobotTerapan,
                request.BobotKerjasama,
                request.BobotPenelitianDasar,
                request.Skor
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            inovasiPemecahanMasalahRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
