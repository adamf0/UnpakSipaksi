using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Domain.JumlahKolaboratorPublikasBereputasi;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.UpdateJumlahKolaboratorPublikasBereputasi
{
    internal sealed class UpdateJumlahKolaboratorPublikasBereputasiCommandHandler(
    IJumlahKolaboratorPublikasBereputasiRepository jumlahKolaboratorPublikasBereputasiRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateJumlahKolaboratorPublikasBereputasiCommand>
    {
        public async Task<Result> Handle(UpdateJumlahKolaboratorPublikasBereputasiCommand request, CancellationToken cancellationToken)
        {
            Domain.JumlahKolaboratorPublikasBereputasi.JumlahKolaboratorPublikasBereputasi? existingJumlahKolaboratorPublikasBereputasi = await jumlahKolaboratorPublikasBereputasiRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingJumlahKolaboratorPublikasBereputasi is null)
            {
                Result.Failure(JumlahKolaboratorPublikasBereputasiErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.JumlahKolaboratorPublikasBereputasi.JumlahKolaboratorPublikasBereputasi> asset = Domain.JumlahKolaboratorPublikasBereputasi.JumlahKolaboratorPublikasBereputasi.Update(existingJumlahKolaboratorPublikasBereputasi!)
                         .ChangeNama(request.Nama)
                         .ChangeSkor(request.Skor)
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
