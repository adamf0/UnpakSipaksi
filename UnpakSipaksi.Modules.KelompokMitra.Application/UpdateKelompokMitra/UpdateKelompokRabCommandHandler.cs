using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KelompokMitra.Domain.KelompokMitra;
using UnpakSipaksi.Modules.KelompokMitra.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KelompokMitra.Application.UpdateKelompokMitra
{
    internal sealed class UpdateKelompokMitraCommandHandler(
    IKelompokMitraRepository kelompokMitraRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKelompokMitraCommand>
    {
        public async Task<Result> Handle(UpdateKelompokMitraCommand request, CancellationToken cancellationToken)
        {
            Domain.KelompokMitra.KelompokMitra? existingKelompokMitra = await kelompokMitraRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingKelompokMitra is null)
            {
                Result.Failure(KelompokMitraErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.KelompokMitra.KelompokMitra> asset = Domain.KelompokMitra.KelompokMitra.Update(existingKelompokMitra!)
                         .ChangeNama(request.Nama)
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
