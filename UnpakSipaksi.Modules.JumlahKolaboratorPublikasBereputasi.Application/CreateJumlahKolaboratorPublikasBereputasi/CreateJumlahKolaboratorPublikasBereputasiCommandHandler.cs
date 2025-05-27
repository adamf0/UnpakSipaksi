using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Domain.JumlahKolaboratorPublikasBereputasi;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.CreateJumlahKolaboratorPublikasBereputasi
{
    internal sealed class CreateJumlahKolaboratorPublikasBereputasiCommandHandler(
    IJumlahKolaboratorPublikasBereputasiRepository akurasiPenelitianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateJumlahKolaboratorPublikasBereputasiCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateJumlahKolaboratorPublikasBereputasiCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.JumlahKolaboratorPublikasBereputasi.JumlahKolaboratorPublikasBereputasi> result = Domain.JumlahKolaboratorPublikasBereputasi.JumlahKolaboratorPublikasBereputasi.Create(
                request.Nama,
                request.Skor
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            akurasiPenelitianRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
