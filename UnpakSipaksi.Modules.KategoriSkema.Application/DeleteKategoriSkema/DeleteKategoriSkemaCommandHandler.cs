using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriSkema.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriSkema.Domain.KategoriSkema;

namespace UnpakSipaksi.Modules.KategoriSkema.Application.DeleteKategoriSkema
{
    internal sealed class DeleteKategoriSkemaCommandHandler(
    IKategoriSkemaRepository KategoriSkemaRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKategoriSkemaCommand>
    {
        public async Task<Result> Handle(DeleteKategoriSkemaCommand request, CancellationToken cancellationToken)
        {
            Domain.KategoriSkema.KategoriSkema? existingKategoriSkema = await KategoriSkemaRepository.GetAsync(request.uuid, cancellationToken);

            if (existingKategoriSkema is null)
            {
                return Result.Failure(KategoriSkemaErrors.NotFound(request.uuid));
            }

            await KategoriSkemaRepository.DeleteAsync(existingKategoriSkema!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
