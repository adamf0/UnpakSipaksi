using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberNonDosen;
using UnpakSipaksi.Modules.PenelitianPkm.PublicApi;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.CreateMemberNonDosen
{
    internal sealed class CreateMemberNonDosenCommandHandler(
        IPenelitianPkmApi penelitianHibahApi,
        IMemberNonDosenRepository memberRepository,
        IUnitOfWorkNonMember unitOfWork)
        : ICommandHandler<CreateMemberNonDosenCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateMemberNonDosenCommand request, CancellationToken cancellationToken)
        {
            PenelitianPkmResponse? existData = await penelitianHibahApi.GetAsync(Guid.Parse(request.UuidPenelitianPkm));
            if (existData == null)
            {
                return Result.Failure<Guid>(MemberNonDosenErrors.NotFoundHibah(Guid.Parse(request.UuidPenelitianPkm)));
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
