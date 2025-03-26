using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriSkema.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriSkema.Domain.KategoriSkema;

namespace UnpakSipaksi.Modules.KategoriSkema.Application.CreateKategoriSkema
{
    internal sealed class CreateKategoriSkemaCommandHandler(
    IKategoriSkemaRepository KategoriSkemaRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKategoriSkemaCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKategoriSkemaCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KategoriSkema.KategoriSkema> result = Domain.KategoriSkema.KategoriSkema.Create(
                request.Nama,
                request.Rule
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            KategoriSkemaRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
