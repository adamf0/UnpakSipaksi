using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Domain.ArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Application.DeleteArtikelMediaMassa
{
    internal sealed class DeleteArtikelMediaMassaCommandHandler(
    IArtikelMediaMassaRepository artikelMediaMassaRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteArtikelMediaMassaCommand>
    {
        public async Task<Result> Handle(DeleteArtikelMediaMassaCommand request, CancellationToken cancellationToken)
        {
            Domain.ArtikelMediaMassa.ArtikelMediaMassa? existingArtikelMediaMassa = await artikelMediaMassaRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingArtikelMediaMassa is null)
            {
                return Result.Failure(ArtikelMediaMassaErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await artikelMediaMassaRepository.DeleteAsync(existingArtikelMediaMassa!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
