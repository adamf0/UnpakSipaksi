using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.Substansi;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.PublicApi;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateKategoriPengabdian
{
    internal sealed class UpdateKategoriPengabdianCommandHandler(
        IKategoriProgramPengabdianApi kategoriProgramPengabdianApi,
        IPenelitianPkmRepository penelitianHibahRepository,
        IUnitOfWorkSubstansi unitOfWork)
        : ICommandHandler<UpdateKategoriPengabdianCommand>
    {
        public async Task<Result> Handle(UpdateKategoriPengabdianCommand request, CancellationToken cancellationToken)
        {
            KategoriProgramPengabdianResponse? existingKategoriProgramPengabdian = await kategoriProgramPengabdianApi.GetAsync(Guid.Parse(request.UuidKategoriPengabdian));
            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianPkm), cancellationToken);
            
            Result<Domain.PenelitianPkm.PenelitianPkm> updateResult = Domain.PenelitianPkm.PenelitianPkm.UpdateKategoriProgramPengabdian(
                existingPenelitianPkm,
                int.Parse(existingKategoriProgramPengabdian?.Id ?? "0")
            );

            if (updateResult.IsFailure)
                return Result.Failure(updateResult.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
