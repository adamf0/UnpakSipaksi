using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianPkm.PublicApi;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.CreateMemberMahasiswa
{
    internal sealed class CreateMemberMahasiswaCommandHandler(
        IPenelitianPkmApi penelitianHibahApi,
        IMemberMahasiswaRepository memberRepository,
        IUnitOfWorkMemberMahasiswa unitOfWork)
        : ICommandHandler<CreateMemberMahasiswaCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateMemberMahasiswaCommand request, CancellationToken cancellationToken)
        {
            //harus pindah ke domain
            PenelitianPkmResponse? existData = await penelitianHibahApi.GetAsync(Guid.Parse(request.UuidPenelitianPkm));
            if (existData == null)
            {
                return Result.Failure<Guid>(MemberMahasiswaErrors.NotFoundHibah(Guid.Parse(request.UuidPenelitianPkm)));
            }

            int checkData = await memberRepository.CheckUniqueDataAsync(int.Parse(existData!.Id), request.NPM, cancellationToken);

            if (checkData > 0)
            {
                return Result.Failure<Guid>(MemberMahasiswaErrors.NotUnique(request.NPM));
            }
            //harus pindah ke domain

            //[PR] check valid npm

            Result<Domain.MemberMahasiswa.MemberMahasiswa> result = Domain.MemberMahasiswa.MemberMahasiswa.Create(
                int.Parse(existData!.Id),
                request.NPM
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
