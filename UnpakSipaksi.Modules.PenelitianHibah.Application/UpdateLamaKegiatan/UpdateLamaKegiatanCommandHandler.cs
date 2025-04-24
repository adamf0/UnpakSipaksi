using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateLamaKegiatan
{
    internal sealed class UpdateLamaKegiatanCommandHandler(
        IPenelitianHibahRepository penelitianHibahRepository,
        IUnitOfWorkHibah unitOfWork)
        : ICommandHandler<UpdateMemberDosenCommand>
    {
        public async Task<Result> Handle(UpdateMemberDosenCommand request, CancellationToken cancellationToken)
        {
            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            var createResult = Domain.PenelitianHibah.PenelitianHibah.UpdateLamaKegiatan(
                existingPenelitianHibah,
                request.LamaKegiatan
            );

            if (createResult.IsFailure)
                return Result.Failure<Guid>(createResult.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
