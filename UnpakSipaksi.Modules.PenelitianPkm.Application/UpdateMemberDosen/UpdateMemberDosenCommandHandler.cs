using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateMemberDosen
{
    internal sealed class UpdateMemberDosenCommandHandler(
        IMemberDosenRepository memberRepository,
        IUnitOfWorkMember unitOfWork)
        : ICommandHandler<UpdateMemberDosenCommand>
    {
        public async Task<Result> Handle(UpdateMemberDosenCommand request, CancellationToken cancellationToken)
        {
            MemberDosen? existingMemberDosen = await memberRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingMemberDosen is null)
            {
                return Result.Failure(PenelitianPkmErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            //[PR] check valid nidn

            int checkData = await memberRepository.CheckUniqueDataAsync(existingMemberDosen.Id ?? 0, request.NIDN, cancellationToken);

            if (checkData > 0)
            {
                return Result.Failure<Guid>(MemberDosenErrors.NotUnique(request.NIDN));
            }

            Result<MemberDosen> result = MemberDosen.Update(
                existingMemberDosen!,
                request.NIDN
            );

            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
