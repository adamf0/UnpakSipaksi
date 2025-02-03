using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPengabdian.Domain.FokusPengabdian;
using UnpakSipaksi.Modules.FokusPengabdian.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.FokusPengabdian.Application.DeleteFokusPengabdian
{
    internal sealed class DeleteFokusPengabdianCommandHandler(
    IFokusPengabdianRepository fokusPengabdianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteFokusPengabdianCommand>
    {
        public async Task<Result> Handle(DeleteFokusPengabdianCommand request, CancellationToken cancellationToken)
        {
            Domain.FokusPengabdian.FokusPengabdian? existingFokusPengabdian = await fokusPengabdianRepository.GetAsync(request.uuid, cancellationToken);

            if (existingFokusPengabdian is null)
            {
                return Result.Failure(FokusPengabdianErrors.NotFound(request.uuid));
            }

            await fokusPengabdianRepository.DeleteAsync(existingFokusPengabdian!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
