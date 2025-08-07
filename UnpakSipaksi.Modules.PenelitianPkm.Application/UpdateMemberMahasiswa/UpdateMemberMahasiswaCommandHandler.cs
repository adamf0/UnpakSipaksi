using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateMemberMahasiswa
{
    internal sealed class UpdateMemberMahasiswaCommandHandler(
        IMemberMahasiswaRepository memberRepository,
        IPenelitianPkmRepository hibahRepository,
        IUnitOfWorkMemberMahasiswa unitOfWork)
        : ICommandHandler<UpdateMemberMahasiswaCommand>
    {
        public async Task<Result> Handle(UpdateMemberMahasiswaCommand request, CancellationToken cancellationToken)
        {
            MemberMahasiswa? existingMemberMahasiswa = await memberRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingMemberMahasiswa is null)
            {
                return Result.Failure(PenelitianPkmErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            int checkData = await memberRepository.CheckUniqueDataAsync(existingMemberMahasiswa.Id ?? 0, request.NPM, cancellationToken);
            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm = await hibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianPkm), cancellationToken);

            Result<MemberMahasiswa> result = MemberMahasiswa.Update(
                checkData,
                existingMemberMahasiswa!,
                existingPenelitianPkm,
                request.NPM
            );

            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
