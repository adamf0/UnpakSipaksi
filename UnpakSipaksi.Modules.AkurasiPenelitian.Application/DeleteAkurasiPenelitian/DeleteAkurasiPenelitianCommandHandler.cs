using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.AkurasiPenelitian.Domain.AkurasiPenelitian;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Application.DeleteAkurasiPenelitian
{
    internal sealed class DeleteAkurasiPenelitianCommandHandler(
    IAkurasiPenelitianRepository akurasiPenelitianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteAkurasiPenelitianCommand>
    {
        public async Task<Result> Handle(DeleteAkurasiPenelitianCommand request, CancellationToken cancellationToken)
        {
            Domain.AkurasiPenelitian.AkurasiPenelitian? existingAkurasiPenelitian = await akurasiPenelitianRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingAkurasiPenelitian is null)
            {
                return Result.Failure(AkurasiPenelitianErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await akurasiPenelitianRepository.DeleteAsync(existingAkurasiPenelitian!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
