using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberNonDosen;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.DeleteMemberNonDosen
{
    internal sealed class DeleteMemberNonDosenCommandHandler(
    IMemberNonDosenRepository memberNonDosenRepository,
    IUnitOfWorkNonMember unitOfWork)
    : ICommandHandler<DeleteMemberNonDosenCommand>
    {
        public async Task<Result> Handle(DeleteMemberNonDosenCommand request, CancellationToken cancellationToken)
        {
            Domain.MemberNonDosen.MemberNonDosen? existingMemberNonDosen = await memberNonDosenRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingMemberNonDosen is null)
            {
                return Result.Failure(PenelitianPkmErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            await memberNonDosenRepository.DeleteAsync(existingMemberNonDosen!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
