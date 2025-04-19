using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Domain.InovasiPemecahanMasalah;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.UpdateInovasiPemecahanMasalah
{
    internal sealed class UpdateInovasiPemecahanMasalahCommandHandler(
    IInovasiPemecahanMasalahRepository inovasiPemecahanMasalahRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateInovasiPemecahanMasalahCommand>
    {
        public async Task<Result> Handle(UpdateInovasiPemecahanMasalahCommand request, CancellationToken cancellationToken)
        {
            Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah? existingInovasiPemecahanMasalah = await inovasiPemecahanMasalahRepository.GetAsync(request.Uuid, cancellationToken);

            if (existingInovasiPemecahanMasalah is null)
            {
                Result.Failure(InovasiPemecahanMasalahErrors.NotFound(request.Uuid));
            }

            Result<Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah> asset = Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah.Update(existingInovasiPemecahanMasalah!)
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
