using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberDosen;
using UnpakSipaksi.Modules.PenelitianHibah.PublicApi;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.CreateMemberDosen
{
    internal sealed class CreateMemberDosenCommandHandler(
        IPenelitianHibahApi penelitianHibahApi,
        IMemberDosenRepository memberRepository,
        IUnitOfWorkMember unitOfWork)
        : ICommandHandler<CreateMemberDosenCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateMemberDosenCommand request, CancellationToken cancellationToken)
        {
            PenelitianHibahResponse? existData = await penelitianHibahApi.GetAsync(Guid.Parse(request.UuidPenelitianHibah));
            if (existData == null)
            {
                return Result.Failure<Guid>(MemberDosenErrors.NotFoundHibah(Guid.Parse(request.UuidPenelitianHibah)));
            }

            int checkData = await memberRepository.CheckUniqueDataAsync(int.Parse(existData!.Id), request.NIDN, cancellationToken);
            Result<Domain.MemberDosen.MemberDosen> result = Domain.MemberDosen.MemberDosen.Create(
                checkData,
                int.Parse(existData!.Id),
                request.NIDN
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            memberRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
