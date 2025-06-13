using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Administrasi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Administrasi.Domain.AdministrasiInternal;

namespace UnpakSipaksi.Modules.Administrasi.Application.DeleteAdministrasiInternal
{
    internal sealed class DeleteAdministrasiInternalCommandHandler(
    IAdministrasiInternalRepository akurasiPenelitianRepository,
    IUnitOfWorkAdministrasiInternal unitOfWork)
    : ICommandHandler<DeleteAdministrasiInternalCommand>
    {
        public async Task<Result> Handle(DeleteAdministrasiInternalCommand request, CancellationToken cancellationToken)
        {
            Domain.AdministrasiInternal.AdministrasiInternal? existingAdministrasiInternal = await akurasiPenelitianRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingAdministrasiInternal is null)
            {
                return Result.Failure(AdministrasiInternalErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await akurasiPenelitianRepository.DeleteAsync(existingAdministrasiInternal!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
