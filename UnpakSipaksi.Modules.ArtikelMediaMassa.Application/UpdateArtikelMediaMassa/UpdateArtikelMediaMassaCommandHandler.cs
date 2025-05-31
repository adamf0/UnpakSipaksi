using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Domain.ArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Application.UpdateArtikelMediaMassa
{
    internal sealed class UpdateArtikelMediaMassaCommandHandler(
    IArtikelMediaMassaRepository artikelMediaMassaRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateArtikelMediaMassaCommand>
    {
        public async Task<Result> Handle(UpdateArtikelMediaMassaCommand request, CancellationToken cancellationToken)
        {
            Domain.ArtikelMediaMassa.ArtikelMediaMassa? existingArtikelMediaMassa = await artikelMediaMassaRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingArtikelMediaMassa is null)
            {
                return Result.Failure(ArtikelMediaMassaErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.ArtikelMediaMassa.ArtikelMediaMassa> asset = Domain.ArtikelMediaMassa.ArtikelMediaMassa.Update(existingArtikelMediaMassa!)
                         .ChangeNama(request.Nama)
                         .ChangeNilai(request.Nilai)
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
