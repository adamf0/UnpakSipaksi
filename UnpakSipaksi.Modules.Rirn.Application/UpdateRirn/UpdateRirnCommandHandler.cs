using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Rirn.Domain.Rirn;
using UnpakSipaksi.Modules.Rirn.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.Rirn.Application.UpdateRirn
{
    internal sealed class UpdateRirnCommandHandler(
    IRirnRepository RirnRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateRirnCommand>
    {
        public async Task<Result> Handle(UpdateRirnCommand request, CancellationToken cancellationToken)
        {
            Domain.Rirn.Rirn? existingRirn = await RirnRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingRirn is null)
            {
                return Result.Failure(RirnErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.Rirn.Rirn> asset = Domain.Rirn.Rirn.Update(existingRirn!)
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
