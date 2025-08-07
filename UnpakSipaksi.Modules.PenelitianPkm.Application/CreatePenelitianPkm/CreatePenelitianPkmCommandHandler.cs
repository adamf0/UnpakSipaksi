using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.CreatePenelitianPkm
{
    internal sealed class CreatePenelitianPkmCommandHandler(
        IPenelitianPkmRepository penelitianHibahRepository,
        IUnitOfWorkHibah unitOfWork)
        : ICommandHandler<CreatePenelitianPkmCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreatePenelitianPkmCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.PenelitianPkm.PenelitianPkm> createResult = await Domain.PenelitianPkm.PenelitianPkm.Create(
                penelitianHibahRepository,
                request.NIDN,
                request.TahunPengajuan,
                request.Judul
            );

            if (createResult.IsFailure)
                return Result.Failure<Guid>(createResult.Error);

            penelitianHibahRepository.Insert(createResult.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return createResult.Value.Uuid;
        }
    }
}
