using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateMemberDosen
{
    internal sealed class UpdateMemberDosenCommandHandler(
        IMemberDosenRepository memberRepository,
        IPenelitianHibahRepository hibahRepository,
        IUnitOfWorkMember unitOfWork)
        : ICommandHandler<UpdateMemberDosenCommand>
    {
        public async Task<Result> Handle(UpdateMemberDosenCommand request, CancellationToken cancellationToken)
        {
            MemberDosen? existingMemberDosen = await memberRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingMemberDosen is null)
            {
                return Result.Failure(PenelitianHibahErrors.NotFound(Guid.Parse(request.Uuid)));
            }
            
            int checkData = await memberRepository.CheckUniqueDataAsync(existingMemberDosen.Id??0, request.NIDN, cancellationToken);
            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await hibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianHibah), cancellationToken);

            //[PR] check valid nidn
            Result<MemberDosen> result = MemberDosen.Update(
                checkData,
                existingMemberDosen!,
                existingPenelitianHibah,
                request.NIDN
            );

            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
