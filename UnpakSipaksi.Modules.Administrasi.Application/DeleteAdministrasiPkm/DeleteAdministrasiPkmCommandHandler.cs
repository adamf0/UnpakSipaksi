using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Administrasi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Administrasi.Domain.AdministrasiPkm;

namespace UnpakSipaksi.Modules.Administrasi.Application.DeleteAdministrasiPkm
{
    internal sealed class DeleteAdministrasiPkmCommandHandler(
    IAdministrasiPkmRepository akurasiPenelitianRepository,
    IUnitOfWorkAdministrasiPkm unitOfWork)
    : ICommandHandler<DeleteAdministrasiPkmCommand>
    {
        public async Task<Result> Handle(DeleteAdministrasiPkmCommand request, CancellationToken cancellationToken)
        {
            Domain.AdministrasiPkm.AdministrasiPkm? existingAdministrasiPkm = await akurasiPenelitianRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingAdministrasiPkm is null)
            {
                return Result.Failure(AdministrasiPkmErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await akurasiPenelitianRepository.DeleteAsync(existingAdministrasiPkm!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
