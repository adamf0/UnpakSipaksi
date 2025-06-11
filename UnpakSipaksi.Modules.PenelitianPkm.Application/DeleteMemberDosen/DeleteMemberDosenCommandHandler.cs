using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.DeleteMemberDosen
{
    internal sealed class DeleteMemberDosenCommandHandler(
    IMemberDosenRepository memberDosenRepository,
    IUnitOfWorkMember unitOfWork)
    : ICommandHandler<DeleteMemberDosenCommand>
    {
        public async Task<Result> Handle(DeleteMemberDosenCommand request, CancellationToken cancellationToken)
        {
            Domain.MemberDosen.MemberDosen? existingMemberDosen = await memberDosenRepository.GetAsync(Guid.Parse(request.Uuid), request.NIDN, cancellationToken);

            if (existingMemberDosen is null)
            {
                return Result.Failure(PenelitianPkmErrors.NotFound(Guid.Parse(request.Uuid)));
            }
            if (existingMemberDosen.NIDN != request.NIDN)
            {
                return Result.Failure(PenelitianPkmErrors.InvalidData());
            }

            await memberDosenRepository.DeleteAsync(existingMemberDosen!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
