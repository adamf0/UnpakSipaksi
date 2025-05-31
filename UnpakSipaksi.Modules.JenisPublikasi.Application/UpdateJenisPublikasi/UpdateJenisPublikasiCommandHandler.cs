using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JenisPublikasi.Domain.JenisPublikasi;
using UnpakSipaksi.Modules.JenisPublikasi.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.JenisPublikasi.Application.UpdateJenisPublikasi
{
    internal sealed class UpdateJenisPublikasiCommandHandler(
    IJenisPublikasiRepository jenisPublikasiRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateJenisPublikasiCommand>
    {
        public async Task<Result> Handle(UpdateJenisPublikasiCommand request, CancellationToken cancellationToken)
        {
            Domain.JenisPublikasi.JenisPublikasi? existingJenisPublikasi = await jenisPublikasiRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingJenisPublikasi is null)
            {
                return Result.Failure(JenisPublikasiErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.JenisPublikasi.JenisPublikasi> asset = Domain.JenisPublikasi.JenisPublikasi.Update(existingJenisPublikasi!)
                         .ChangeNama(request.Nama)
                         .ChangeSbu(request.Sbu)
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
