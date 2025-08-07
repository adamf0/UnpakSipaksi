using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetPenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.PublicApi;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.CreateMemberDosen
{
    internal sealed class CreateMemberDosenCommandHandler(
        IPenelitianPkmApi penelitianPkmApi,
        IMemberDosenRepository memberRepository,
        IUnitOfWorkMember unitOfWork)
        : ICommandHandler<CreateMemberDosenCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateMemberDosenCommand request, CancellationToken cancellationToken)
        {
            PublicApi.PenelitianPkmResponse? existData = await penelitianPkmApi.GetAsync(Guid.Parse(request.UuidPenelitianPkm));
            if (existData == null)
            {
                return Result.Failure<Guid>(MemberDosenErrors.NotFoundHibah(Guid.Parse(request.UuidPenelitianPkm)));
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
