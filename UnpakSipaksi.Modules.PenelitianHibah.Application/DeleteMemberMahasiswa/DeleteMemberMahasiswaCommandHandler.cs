using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberMahasiswa;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteMemberMahasiswa
{
    internal sealed class DeleteMemberMahasiswaCommandHandler(
    IMemberMahasiswaRepository memberMahasiswaRepository,
    IUnitOfWorkMemberMahasiswa unitOfWork)
    : ICommandHandler<DeleteMemberMahasiswaCommand>
    {
        public async Task<Result> Handle(DeleteMemberMahasiswaCommand request, CancellationToken cancellationToken)
        {
            Domain.MemberMahasiswa.MemberMahasiswa? existingMemberMahasiswa = await memberMahasiswaRepository.GetAsync(Guid.Parse(request.Uuid), request.Npm, cancellationToken);

            if (existingMemberMahasiswa is null)
            {
                return Result.Failure(PenelitianHibahErrors.NotFound(Guid.Parse(request.Uuid)));
            }
            if (existingMemberMahasiswa.NPM != request.Npm)
            {
                return Result.Failure(PenelitianHibahErrors.InvalidData());
            }

            await memberMahasiswaRepository.DeleteAsync(existingMemberMahasiswa!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
