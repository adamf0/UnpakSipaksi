using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriTkt.Domain.KategoriTkt;
using UnpakSipaksi.Modules.KategoriTkt.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KategoriTkt.Application.DeleteKategoriTkt
{
    internal sealed class DeleteKategoriTktCommandHandler(
    IKategoriTktRepository kategoriTktRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKategoriTktCommand>
    {
        public async Task<Result> Handle(DeleteKategoriTktCommand request, CancellationToken cancellationToken)
        {
            Domain.KategoriTkt.KategoriTkt? existingKategoriTkt = await kategoriTktRepository.GetAsync(request.uuid, cancellationToken);

            if (existingKategoriTkt is null)
            {
                return Result.Failure(KategoriTktErrors.NotFound(request.uuid));
            }

            await kategoriTktRepository.DeleteAsync(existingKategoriTkt!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
