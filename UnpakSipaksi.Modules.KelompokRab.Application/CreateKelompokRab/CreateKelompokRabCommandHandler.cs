using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KelompokRab.Domain.KelompokRab;
using UnpakSipaksi.Modules.KelompokRab.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KelompokRab.Application.CreateKelompokRab
{
    internal sealed class CreateKelompokRabCommandHandler(
    IKelompokRabRepository kelompokRabRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKelompokRabCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKelompokRabCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KelompokRab.KelompokRab> result = Domain.KelompokRab.KelompokRab.Create(
                request.Nama
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            kelompokRabRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
