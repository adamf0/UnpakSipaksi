using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Domain.KejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.DeleteKejelasanPembagianTugasTim
{
    internal sealed class DeleteKejelasanPembagianTugasTimCommandHandler(
    IKejelasanPembagianTugasTimRepository kejelasanPembagianTugasTimRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKejelasanPembagianTugasTimCommand>
    {
        public async Task<Result> Handle(DeleteKejelasanPembagianTugasTimCommand request, CancellationToken cancellationToken)
        {
            Domain.KejelasanPembagianTugasTim.KejelasanPembagianTugasTim? existingKejelasanPembagianTugasTim = await kejelasanPembagianTugasTimRepository.GetAsync(request.uuid, cancellationToken);

            if (existingKejelasanPembagianTugasTim is null)
            {
                return Result.Failure(KejelasanPembagianTugasTimErrors.NotFound(request.uuid));
            }

            await kejelasanPembagianTugasTimRepository.DeleteAsync(existingKejelasanPembagianTugasTim!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
