using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Domain.KategoriProgramPengabdian;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.DeleteKategoriProgramPengabdian
{
    internal sealed class DeleteKategoriProgramPengabdianCommandHandler(
    IKategoriProgramPengabdianRepository KategoriProgramPengabdianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKategoriProgramPengabdianCommand>
    {
        public async Task<Result> Handle(DeleteKategoriProgramPengabdianCommand request, CancellationToken cancellationToken)
        {
            Domain.KategoriProgramPengabdian.KategoriProgramPengabdian? existingKategoriProgramPengabdian = await KategoriProgramPengabdianRepository.GetAsync(request.uuid, cancellationToken);

            if (existingKategoriProgramPengabdian is null)
            {
                return Result.Failure(KategoriProgramPengabdianErrors.NotFound(request.uuid));
            }

            await KategoriProgramPengabdianRepository.DeleteAsync(existingKategoriProgramPengabdian!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
