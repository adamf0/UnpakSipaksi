using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Domain.KejelasanPembagianTugasTim;
using UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Application.CreateKejelasanPembagianTugasTim
{
    internal sealed class CreateKejelasanPembagianTugasTimCommandHandler(
    IKejelasanPembagianTugasTimRepository kebaruanReferensiRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKejelasanPembagianTugasTimCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKejelasanPembagianTugasTimCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KejelasanPembagianTugasTim.KejelasanPembagianTugasTim> result = Domain.KejelasanPembagianTugasTim.KejelasanPembagianTugasTim.Create(
                request.Nama,
                request.Skor
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            kebaruanReferensiRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
