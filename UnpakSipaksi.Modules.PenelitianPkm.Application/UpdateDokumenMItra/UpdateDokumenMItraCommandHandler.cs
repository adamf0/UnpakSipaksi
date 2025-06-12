using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KelompokMitra.PublicApi;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenMitra;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenMitra;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateDokumenMitra
{
    internal sealed class UpdateDokumenMitraCommandHandler(
        IDokumenMitraRepository dokumenMitraRepository,
        IKelompokMitraApi kelompokMitraApi,
        IPenelitianPkmRepository penelitianPkmRepository,
        IUnitOfWorkDokumenMitra unitOfWork)
        : ICommandHandler<UpdateDokumenMitraCommand>
    {
        public async Task<Result> Handle(UpdateDokumenMitraCommand request, CancellationToken cancellationToken)
        {
            Domain.DokumenMitra.DokumenMitra? existingDokumenMitra = await dokumenMitraRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);
            if (existingDokumenMitra is null)
            {
                return Result.Failure(DokumenMitraErrors.NotFound(Guid.Parse(request.Uuid)));
            }
            KelompokMitraResponse? existingKelompokMitra = await kelompokMitraApi.GetAsync(Guid.Parse(request.UuidKelompokMitra));

            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm = await penelitianPkmRepository.GetAsync(Guid.Parse(request.UuidPenelitianPkm), cancellationToken);

            var createResult = Domain.DokumenMitra.DokumenMitra.Update(
                Guid.Parse(request.Uuid),
                Guid.Parse(request.UuidPenelitianPkm),
                existingPenelitianPkm,
                existingDokumenMitra,
                request.Mitra,
                request.Provinsi,
                request.Kota,
                int.Parse(existingKelompokMitra?.Id ?? "0"),
                request.PemimpinMitra,
                request.KontakMitra,
                request.File
            );

            if (createResult.IsFailure)
                return Result.Failure<Guid>(createResult.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(createResult.Value.Uuid);
        }
    }
}
