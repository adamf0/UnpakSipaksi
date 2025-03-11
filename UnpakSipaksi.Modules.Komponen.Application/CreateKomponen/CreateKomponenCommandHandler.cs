using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Komponen.Domain.Komponen;
using UnpakSipaksi.Modules.Komponen.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.Komponen.Application.CreateKomponen
{
    internal sealed class CreateKomponenCommandHandler(
    IKomponenRepository KomponenRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKomponenCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKomponenCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.Komponen.Komponen> result = Domain.Komponen.Komponen.Create(
                request.Nama,
                request.MaxBiaya
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            KomponenRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
