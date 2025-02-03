using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Domain.KejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.UpdateKejelasanPembagianTugasTim
{
    internal sealed class UpdateKejelasanPembagianTugasTimCommandHandler(
    IKejelasanPembagianTugasTimRepository kejelasanPembagianTugasTimRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKejelasanPembagianTugasTimCommand>
    {
        public async Task<Result> Handle(UpdateKejelasanPembagianTugasTimCommand request, CancellationToken cancellationToken)
        {
            Domain.KejelasanPembagianTugasTim.KejelasanPembagianTugasTim? existingKejelasanPembagianTugasTim = await kejelasanPembagianTugasTimRepository.GetAsync(request.Uuid, cancellationToken);

            if (existingKejelasanPembagianTugasTim is null)
            {
                Result.Failure(KejelasanPembagianTugasTimErrors.NotFound(request.Uuid));
            }

            Result<Domain.KejelasanPembagianTugasTim.KejelasanPembagianTugasTim> asset = Domain.KejelasanPembagianTugasTim.KejelasanPembagianTugasTim.Update(existingKejelasanPembagianTugasTim!)
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
