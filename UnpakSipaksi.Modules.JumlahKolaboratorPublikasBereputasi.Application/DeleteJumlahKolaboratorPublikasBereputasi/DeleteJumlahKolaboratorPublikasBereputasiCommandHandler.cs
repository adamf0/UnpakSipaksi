using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Domain.JumlahKolaboratorPublikasBereputasi;
using UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Application.DeleteJumlahKolaboratorPublikasBereputasi
{
    internal sealed class DeleteJumlahKolaboratorPublikasBereputasiCommandHandler(
    IJumlahKolaboratorPublikasBereputasiRepository jumlahKolaboratorPublikasBereputasiRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteJumlahKolaboratorPublikasBereputasiCommand>
    {
        public async Task<Result> Handle(DeleteJumlahKolaboratorPublikasBereputasiCommand request, CancellationToken cancellationToken)
        {
            Domain.JumlahKolaboratorPublikasBereputasi.JumlahKolaboratorPublikasBereputasi? existingJumlahKolaboratorPublikasBereputasi = await jumlahKolaboratorPublikasBereputasiRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingJumlahKolaboratorPublikasBereputasi is null)
            {
                return Result.Failure(JumlahKolaboratorPublikasBereputasiErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await jumlahKolaboratorPublikasBereputasiRepository.DeleteAsync(existingJumlahKolaboratorPublikasBereputasi!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
