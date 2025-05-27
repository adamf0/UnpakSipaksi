using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JenisPublikasi.Domain.JenisPublikasi;
using UnpakSipaksi.Modules.JenisPublikasi.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.JenisPublikasi.Application.DeleteJenisPublikasi
{
    internal sealed class DeleteJenisPublikasiCommandHandler(
    IJenisPublikasiRepository jenisPublikasiRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteJenisPublikasiCommand>
    {
        public async Task<Result> Handle(DeleteJenisPublikasiCommand request, CancellationToken cancellationToken)
        {
            Domain.JenisPublikasi.JenisPublikasi? existingJenisPublikasi = await jenisPublikasiRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingJenisPublikasi is null)
            {
                return Result.Failure(JenisPublikasiErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await jenisPublikasiRepository.DeleteAsync(existingJenisPublikasi!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
