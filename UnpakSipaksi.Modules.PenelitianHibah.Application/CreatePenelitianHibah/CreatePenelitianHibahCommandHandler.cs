using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.CreatePenelitianHibah
{
    internal sealed class CreatePenelitianHibahCommandHandler(
        IPenelitianHibahRepository penelitianHibahRepository,
        IUnitOfWorkHibah unitOfWork)
        : ICommandHandler<CreatePenelitianHibahCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreatePenelitianHibahCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.PenelitianHibah.PenelitianHibah> createResult = await Domain.PenelitianHibah.PenelitianHibah.Create(
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
