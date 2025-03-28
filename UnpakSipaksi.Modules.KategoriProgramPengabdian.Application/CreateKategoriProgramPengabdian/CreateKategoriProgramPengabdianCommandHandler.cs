using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriProgramPengabdian.Domain.KategoriProgramPengabdian;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Application.CreateKategoriProgramPengabdian
{
    internal sealed class CreateKategoriProgramPengabdianCommandHandler(
    IKategoriProgramPengabdianRepository KategoriProgramPengabdianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKategoriProgramPengabdianCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKategoriProgramPengabdianCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KategoriProgramPengabdian.KategoriProgramPengabdian> result = Domain.KategoriProgramPengabdian.KategoriProgramPengabdian.Create(
                request.Nama,
                request.Rule
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            KategoriProgramPengabdianRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
