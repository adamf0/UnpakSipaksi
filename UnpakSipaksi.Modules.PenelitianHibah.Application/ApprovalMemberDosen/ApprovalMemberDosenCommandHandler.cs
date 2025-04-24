using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.ApprovalMemberDosen
{
    internal sealed class ApprovalMemberDosenCommandHandler(
        IMemberDosenRepository memberRepository,
        IUnitOfWorkMember unitOfWork)
        : ICommandHandler<ApprovalMemberDosenCommand>
    {
        public async Task<Result> Handle(ApprovalMemberDosenCommand request, CancellationToken cancellationToken)
        {
            MemberDosen? existingMemberDosen = await memberRepository.GetAsync(
                Guid.Parse(request.Uuid),
                Guid.Parse(request.UuidPenelitianHibah),
                cancellationToken
            );

            if (!new[] { "approve", "reject" }.Contains(request.Status.ToLower()))
            {
                return Result.Failure(PenelitianHibahErrors.InvalidStatusMember());
            }

            //[PR] check valid nidn
            //[PR] check nidn input with token

            Result<MemberDosen> result;
            if (request.Status.ToLower() == "approve") {
                result = MemberDosen.Approve(existingMemberDosen);
            }
            else {
                result = MemberDosen.Reject(existingMemberDosen);
            }

            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
