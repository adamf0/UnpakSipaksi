using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateMemberMahasiswa
{
    internal sealed class UpdateMemberMahasiswaCommandHandler(
        IMemberMahasiswaRepository memberRepository,
        IPenelitianHibahRepository hibahRepository,
        IUnitOfWorkMemberMahasiswa unitOfWork)
        : ICommandHandler<UpdateMemberMahasiswaCommand>
    {
        public async Task<Result> Handle(UpdateMemberMahasiswaCommand request, CancellationToken cancellationToken)
        {
            MemberMahasiswa? existingMemberMahasiswa = await memberRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingMemberMahasiswa is null)
            {
                return Result.Failure(PenelitianHibahErrors.NotFound(Guid.Parse(request.Uuid)));
            }
            
            int checkData = await memberRepository.CheckUniqueDataAsync(existingMemberMahasiswa.Id??0, request.NPM, cancellationToken);
            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await hibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianHibah), cancellationToken);

            Result<MemberMahasiswa> result = MemberMahasiswa.Update(
                checkData,
                existingMemberMahasiswa!,
                existingPenelitianHibah,
                request.NPM
            );

            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
