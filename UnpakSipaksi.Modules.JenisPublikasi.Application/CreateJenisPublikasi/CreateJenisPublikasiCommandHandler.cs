using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JenisPublikasi.Domain.JenisPublikasi;
using UnpakSipaksi.Modules.JenisPublikasi.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.JenisPublikasi.Application.CreateJenisPublikasi
{
    internal sealed class CreateJenisPublikasiCommandHandler(
    IJenisPublikasiRepository jenisPublikasiRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateJenisPublikasiCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateJenisPublikasiCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.JenisPublikasi.JenisPublikasi> result = Domain.JenisPublikasi.JenisPublikasi.Create(
                request.Nama,
                request.Sbu
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            jenisPublikasiRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
