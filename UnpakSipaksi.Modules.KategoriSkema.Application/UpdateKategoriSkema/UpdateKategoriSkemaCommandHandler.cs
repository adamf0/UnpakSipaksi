using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriSkema.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriSkema.Domain.KategoriSkema;

namespace UnpakSipaksi.Modules.KategoriSkema.Application.UpdateKategoriSkema
{
    internal sealed class UpdateKategoriSkemaCommandHandler(
    IKategoriSkemaRepository KategoriSkemaRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKategoriSkemaCommand>
    {
        public async Task<Result> Handle(UpdateKategoriSkemaCommand request, CancellationToken cancellationToken)
        {
            Domain.KategoriSkema.KategoriSkema? existingKategoriSkema = await KategoriSkemaRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingKategoriSkema is null)
            {
                Result.Failure(KategoriSkemaErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.KategoriSkema.KategoriSkema> asset = Domain.KategoriSkema.KategoriSkema.Update(existingKategoriSkema!)
                         .ChangeNama(request.Nama)
                         .ChangeRule(request.Rule)
                         .Build();

            if (asset.IsFailure)
            {
                return Result.Failure<Guid>(asset.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
