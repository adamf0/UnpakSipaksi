using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Domain.ArtikelMediaMassa;
using UnpakSipaksi.Modules.ArtikelMediaMassa.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Application.CreateArtikelMediaMassa
{
    internal sealed class CreateArtikelMediaMassaCommandHandler(
    IArtikelMediaMassaRepository artikelMediaMassaRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateArtikelMediaMassaCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateArtikelMediaMassaCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.ArtikelMediaMassa.ArtikelMediaMassa> result = Domain.ArtikelMediaMassa.ArtikelMediaMassa.Create(
                request.Nama,
                request.Nilai
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            artikelMediaMassaRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
