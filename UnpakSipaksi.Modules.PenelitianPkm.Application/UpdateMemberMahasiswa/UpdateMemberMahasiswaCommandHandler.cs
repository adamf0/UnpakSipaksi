using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateMemberMahasiswa
{
    internal sealed class UpdateMemberMahasiswaCommandHandler(
        IMemberMahasiswaRepository memberRepository,
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

            //[PR] check valid npm

            int checkData = await memberRepository.CheckUniqueDataAsync(existingMemberMahasiswa.Id ?? 0, request.NPM, cancellationToken);

            if (checkData > 0)
            {
                return Result.Failure<Guid>(MemberMahasiswaErrors.NotUnique(request.NPM));
            }

            Result<MemberMahasiswa> result = MemberMahasiswa.Update(
                existingMemberMahasiswa!,
                request.NPM
            );

            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
