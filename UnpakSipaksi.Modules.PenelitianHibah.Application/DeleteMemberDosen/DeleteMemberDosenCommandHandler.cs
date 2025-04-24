using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberDosen;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteMemberDosen
{
    internal sealed class DeleteMemberDosenCommandHandler(
    IMemberDosenRepository memberDosenRepository,
    IUnitOfWorkMember unitOfWork)
    : ICommandHandler<DeleteMemberDosenCommand>
    {
        public async Task<Result> Handle(DeleteMemberDosenCommand request, CancellationToken cancellationToken)
        {
            Domain.MemberDosen.MemberDosen? existingMemberDosen = await memberDosenRepository.GetAsync(request.Uuid, request.NIDN, cancellationToken);

            if (existingMemberDosen is null)
            {
                return Result.Failure(PenelitianHibahErrors.NotFound(request.Uuid));
            }

            await memberDosenRepository.DeleteAsync(existingMemberDosen!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
