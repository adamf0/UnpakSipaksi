using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KelompokMitra.Domain.KelompokMitra;
using UnpakSipaksi.Modules.KelompokMitra.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KelompokMitra.Application.DeleteKelompokMitra
{
    internal sealed class DeleteKelompokMitraCommandHandler(
    IKelompokMitraRepository kelompokMitraRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKelompokMitraCommand>
    {
        public async Task<Result> Handle(DeleteKelompokMitraCommand request, CancellationToken cancellationToken)
        {
            Domain.KelompokMitra.KelompokMitra? existingKelompokMitra = await kelompokMitraRepository.GetAsync(request.uuid, cancellationToken);

            if (existingKelompokMitra is null)
            {
                return Result.Failure(KelompokMitraErrors.NotFound(request.uuid));
            }

            await kelompokMitraRepository.DeleteAsync(existingKelompokMitra!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
