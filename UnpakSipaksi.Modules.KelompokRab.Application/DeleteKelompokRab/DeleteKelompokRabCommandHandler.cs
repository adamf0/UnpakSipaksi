using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KelompokRab.Domain.KelompokRab;
using UnpakSipaksi.Modules.KelompokRab.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KelompokRab.Application.DeleteKelompokRab
{
    internal sealed class DeleteKelompokRabCommandHandler(
    IKelompokRabRepository kelompokRabRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKelompokRabCommand>
    {
        public async Task<Result> Handle(DeleteKelompokRabCommand request, CancellationToken cancellationToken)
        {
            Domain.KelompokRab.KelompokRab? existingKelompokRab = await kelompokRabRepository.GetAsync(request.uuid, cancellationToken);

            if (existingKelompokRab is null)
            {
                return Result.Failure(KelompokRabErrors.NotFound(request.uuid));
            }

            await kelompokRabRepository.DeleteAsync(existingKelompokRab!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
