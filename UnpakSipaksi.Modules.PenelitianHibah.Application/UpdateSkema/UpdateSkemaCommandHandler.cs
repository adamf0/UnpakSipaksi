using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriSkema.PublicApi;
using UnpakSipaksi.Modules.KategoriTkt.PublicApi;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateSkema
{
    internal sealed class UpdateSkemaCommandHandler(
        IPenelitianHibahRepository penelitianHibahRepository,
        IKategoriSkemaApi kategoriSkemaApi,
        IKategoriTktApi kategoriTktApi,
        IUnitOfWorkHibah unitOfWork)
        : ICommandHandler<UpdateSkemaCommand>
    {
        public async Task<Result> Handle(UpdateSkemaCommand request, CancellationToken cancellationToken)
        {
            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianHibah), cancellationToken);

            var kategoriSkema = await kategoriSkemaApi.GetAsync(Guid.Parse(request.SkemaId));
            if (kategoriSkema is null)
                return Result.Failure<Guid>(PenelitianHibahErrors.NotFoundKategoriSkema(request.SkemaId));

            var kategoriTkt = await kategoriTktApi.GetAsync(Guid.Parse(request.KategoriTKT));
            if (kategoriTkt is null)
                return Result.Failure<Guid>(PenelitianHibahErrors.NotFoundKategoriTkt(request.KategoriTKT));

            var createResult = Domain.PenelitianHibah.PenelitianHibah.UpdateSkema(
                existingPenelitianHibah,
                int.Parse(kategoriSkema.Id),
                request.TKT,
                int.Parse(kategoriTkt.Id)
            );

            if (createResult.IsFailure)
                return Result.Failure<Guid>(createResult.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
