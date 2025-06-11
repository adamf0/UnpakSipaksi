using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateMbkm
{
    internal sealed class UpdateMemberMbkmCommandHandler(
        IMemberMahasiswaRepository memberRepository,
        IUnitOfWorkMemberMahasiswa unitOfWork)
        : ICommandHandler<UpdateMbkmCommand>
    {
        public async Task<Result> Handle(UpdateMbkmCommand request, CancellationToken cancellationToken)
        {
            MemberMahasiswa? existingMemberMahasiswa = await memberRepository.GetAsync(
                Guid.Parse(request.Uuid),
                Guid.Parse(request.UuidPenelitianPkm),
                request.NPM,
                cancellationToken
            );

            Result<MemberMahasiswa> result = MemberMahasiswa.UpdateMbkm(
                existingMemberMahasiswa,
                request.BuktiMbkm
            );

            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
