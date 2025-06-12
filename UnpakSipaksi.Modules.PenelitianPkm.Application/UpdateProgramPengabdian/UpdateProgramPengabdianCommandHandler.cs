using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPengabdian.PublicApi;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.Rirn.PublicApi;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateProgramPengabdian
{
    internal sealed class UpdateProgramPengabdianCommandHandler(
        IFokusPengabdianApi fokusPengabdianApi,
        IRirnApi rirnApi,
        IPenelitianPkmRepository penelitianHibahRepository,
        IUnitOfWorkSubstansi unitOfWork)
        : ICommandHandler<UpdateProgramPengabdianCommand>
    {
        public async Task<Result> Handle(UpdateProgramPengabdianCommand request, CancellationToken cancellationToken)
        {
            FokusPengabdianResponse? existingFokusPengabdian = await fokusPengabdianApi.GetAsync(Guid.Parse(request.UuidFokusPengabdian));
            RirnResponse? existingRirn = await rirnApi.GetAsync(Guid.Parse(request.UuidRirn));

            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianPkm), cancellationToken);
            
            Result<Domain.PenelitianPkm.PenelitianPkm> updateResult = Domain.PenelitianPkm.PenelitianPkm.UpdateProgramPengabdian(
                existingPenelitianPkm,
                int.Parse(existingFokusPengabdian?.Id ?? "0"),
                int.Parse(existingRirn?.Id ?? "0")
            );

            if (updateResult.IsFailure)
                return Result.Failure(updateResult.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
