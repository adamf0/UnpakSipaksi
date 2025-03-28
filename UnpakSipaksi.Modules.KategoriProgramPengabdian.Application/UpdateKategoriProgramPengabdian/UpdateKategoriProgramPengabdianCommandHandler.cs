using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Domain.KategoriProgramPengabdian;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.UpdateKategoriProgramPengabdian
{
    internal sealed class UpdateKategoriProgramPengabdianCommandHandler(
    IKategoriProgramPengabdianRepository KategoriProgramPengabdianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKategoriProgramPengabdianCommand>
    {
        public async Task<Result> Handle(UpdateKategoriProgramPengabdianCommand request, CancellationToken cancellationToken)
        {
            Domain.KategoriProgramPengabdian.KategoriProgramPengabdian? existingKategoriProgramPengabdian = await KategoriProgramPengabdianRepository.GetAsync(request.Uuid, cancellationToken);

            if (existingKategoriProgramPengabdian is null)
            {
                Result.Failure(KategoriProgramPengabdianErrors.NotFound(request.Uuid));
            }

            Result<Domain.KategoriProgramPengabdian.KategoriProgramPengabdian> asset = Domain.KategoriProgramPengabdian.KategoriProgramPengabdian.Update(existingKategoriProgramPengabdian!)
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
