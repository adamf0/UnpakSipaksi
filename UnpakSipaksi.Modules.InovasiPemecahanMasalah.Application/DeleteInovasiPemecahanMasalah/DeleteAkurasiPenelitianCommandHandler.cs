using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Domain.InovasiPemecahanMasalah;
using UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Application.DeleteInovasiPemecahanMasalah
{
    internal sealed class DeleteInovasiPemecahanMasalahCommandHandler(
    IInovasiPemecahanMasalahRepository inovasiPemecahanMasalahRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteInovasiPemecahanMasalahCommand>
    {
        public async Task<Result> Handle(DeleteInovasiPemecahanMasalahCommand request, CancellationToken cancellationToken)
        {
            Domain.InovasiPemecahanMasalah.InovasiPemecahanMasalah? existingInovasiPemecahanMasalah = await inovasiPemecahanMasalahRepository.GetAsync(request.uuid, cancellationToken);

            if (existingInovasiPemecahanMasalah is null)
            {
                return Result.Failure(InovasiPemecahanMasalahErrors.NotFound(request.uuid));
            }

            await inovasiPemecahanMasalahRepository.DeleteAsync(existingInovasiPemecahanMasalah!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
