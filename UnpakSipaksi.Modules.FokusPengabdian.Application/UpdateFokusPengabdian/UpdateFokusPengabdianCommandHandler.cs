using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPengabdian.Domain.FokusPengabdian;
using UnpakSipaksi.Modules.FokusPengabdian.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.FokusPengabdian.Application.UpdateFokusPengabdian
{
    internal sealed class UpdateFokusPengabdianCommandHandler(
    IFokusPengabdianRepository fokusPengabdianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateFokusPengabdianCommand>
    {
        public async Task<Result> Handle(UpdateFokusPengabdianCommand request, CancellationToken cancellationToken)
        {
            Domain.FokusPengabdian.FokusPengabdian? existingFokusPengabdian = await fokusPengabdianRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingFokusPengabdian is null)
            {
                Result.Failure(FokusPengabdianErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.FokusPengabdian.FokusPengabdian> asset = Domain.FokusPengabdian.FokusPengabdian.Update(existingFokusPengabdian!)
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
