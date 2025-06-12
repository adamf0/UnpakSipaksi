using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KelompokMitra.PublicApi;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenMitra;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.CreateDokumenMitra
{
    internal sealed class CreateDokumenMitraCommandHandler(
        IDokumenMitraRepository dokumenMitraRepository,
        IKelompokMitraApi kelompokMitraApi,
        IPenelitianPkmRepository penelitianPkmRepository,
        IUnitOfWorkDokumenMitra unitOfWork)
        : ICommandHandler<CreateDokumenMitraCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateDokumenMitraCommand request, CancellationToken cancellationToken)
        {
            KelompokMitraResponse? existingKelompokMitra = await kelompokMitraApi.GetAsync(Guid.Parse(request.UuidKelompokMitra));
            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm = await penelitianPkmRepository.GetAsync(Guid.Parse(request.UuidPenelitianPkm), cancellationToken);

            var result = Domain.DokumenMitra.DokumenMitra.Create(
                Guid.Parse(request.UuidPenelitianPkm),
                existingPenelitianPkm,
                request.Mitra,
                request.Provinsi,
                request.Kota,
                int.Parse(existingKelompokMitra?.Id ?? "0"),
                request.PemimpinMitra,
                request.KontakMitra,
                request.File
            );

            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);

            dokumenMitraRepository.Insert(result.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(result.Value.Uuid);
        }
    }
}
