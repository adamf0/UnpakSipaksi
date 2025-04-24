using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberNonDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateMemberNonDosen
{
    internal sealed class UpdateMemberNonDosenCommandHandler(
        IMemberNonDosenRepository memberRepository,
        IUnitOfWorkNonMember unitOfWork)
        : ICommandHandler<UpdateMemberNonDosenCommand>
    {
        public async Task<Result> Handle(UpdateMemberNonDosenCommand request, CancellationToken cancellationToken)
        {
            MemberNonDosen? existingMemberNonDosen = await memberRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            Result<MemberNonDosen> result = MemberNonDosen.Update(
                existingMemberNonDosen,
                request.NomorIdentitas,
                request.Nama,
                request.Afiliasi
            );

            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
