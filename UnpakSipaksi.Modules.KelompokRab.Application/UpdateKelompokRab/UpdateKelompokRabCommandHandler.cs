using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KelompokRab.Domain.KelompokRab;
using UnpakSipaksi.Modules.KelompokRab.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KelompokRab.Application.UpdateKelompokRab
{
    internal sealed class UpdateKelompokRabCommandHandler(
    IKelompokRabRepository kelompokRabRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKelompokRabCommand>
    {
        public async Task<Result> Handle(UpdateKelompokRabCommand request, CancellationToken cancellationToken)
        {
            Domain.KelompokRab.KelompokRab? existingKelompokRab = await kelompokRabRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingKelompokRab is null)
            {
                return Result.Failure(KelompokRabErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.KelompokRab.KelompokRab> asset = Domain.KelompokRab.KelompokRab.Update(existingKelompokRab!)
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
