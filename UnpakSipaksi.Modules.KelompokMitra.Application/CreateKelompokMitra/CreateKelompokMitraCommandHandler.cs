using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KelompokMitra.Domain.KelompokMitra;
using UnpakSipaksi.Modules.KelompokMitra.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KelompokMitra.Application.CreateKelompokMitra
{
    internal sealed class CreateKelompokMitraCommandHandler(
    IKelompokMitraRepository kelompokMitraRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKelompokMitraCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKelompokMitraCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KelompokMitra.KelompokMitra> result = Domain.KelompokMitra.KelompokMitra.Create(
                request.Nama
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            kelompokMitraRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
