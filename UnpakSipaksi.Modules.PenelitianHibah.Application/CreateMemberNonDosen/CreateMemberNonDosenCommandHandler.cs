using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberNonDosen;
using UnpakSipaksi.Modules.PenelitianHibah.PublicApi;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.CreateMemberNonDosen
{
    internal sealed class CreateMemberNonDosenCommandHandler(
        IPenelitianHibahApi penelitianHibahApi,
        IMemberNonDosenRepository memberRepository,
        IUnitOfWorkNonMember unitOfWork)
        : ICommandHandler<CreateMemberNonDosenCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateMemberNonDosenCommand request, CancellationToken cancellationToken)
        {
            PenelitianHibahResponse? existData = await penelitianHibahApi.GetAsync(Guid.Parse(request.UuidPenelitianHibah));
            if (existData == null)
            {
                return Result.Failure<Guid>(MemberNonDosenErrors.NotFoundHibah(Guid.Parse(request.UuidPenelitianHibah)));
            }

            Result<Domain.MemberNonDosen.MemberNonDosen> result = Domain.MemberNonDosen.MemberNonDosen.Create(
                int.Parse(existData!.Id),
                request.NomorIdentitas,
                request.Nama,
                request.Afiliasi
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
