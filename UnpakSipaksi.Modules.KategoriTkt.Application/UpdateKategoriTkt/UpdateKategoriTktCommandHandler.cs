using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriTkt.Domain.KategoriTkt;
using UnpakSipaksi.Modules.KategoriTkt.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KategoriTkt.Application.UpdateKategoriTkt
{
    internal sealed class UpdateKategoriTktCommandHandler(
    IKategoriTktRepository kategoriTktRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKategoriTktCommand>
    {
        public async Task<Result> Handle(UpdateKategoriTktCommand request, CancellationToken cancellationToken)
        {
            Domain.KategoriTkt.KategoriTkt? existingKategoriTkt = await kategoriTktRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingKategoriTkt is null)
            {
                return Result.Failure(KategoriTktErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.KategoriTkt.KategoriTkt> asset = Domain.KategoriTkt.KategoriTkt.Update(existingKategoriTkt!)
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
