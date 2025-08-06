using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.ApprovalMemberDosen
{
    internal sealed class ApprovalMemberDosenCommandHandler(
        IMemberDosenRepository memberRepository,
        IPenelitianPkmRepository hibahRepository,
        IUnitOfWorkMember unitOfWork)
        : ICommandHandler<ApprovalMemberDosenCommand>
    {
        public async Task<Result> Handle(ApprovalMemberDosenCommand request, CancellationToken cancellationToken)
        {
            MemberDosen? existingMemberDosen = await memberRepository.GetAsync(
                Guid.Parse(request.Uuid),
                Guid.Parse(request.UuidPenelitianPkm),
                cancellationToken
            );

            if (!new[] { "approve", "reject" }.Contains(request.Status.ToLower()))
            {
                return Result.Failure(PenelitianPkmErrors.InvalidStatusMember());
            }
            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm = await hibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianPkm), cancellationToken);

            //[PR] check valid nidn
            //[PR] check nidn input with token

            Result<MemberDosen> result;
            if (request.Status.ToLower() == "approve")
            {
                result = MemberDosen.Approve(existingMemberDosen, existingPenelitianPkm);
            }
            else
            {
                result = MemberDosen.Reject(existingMemberDosen, existingPenelitianPkm);
            }

            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
