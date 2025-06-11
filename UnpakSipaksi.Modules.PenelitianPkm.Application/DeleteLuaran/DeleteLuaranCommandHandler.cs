using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.Luaran;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.DeleteLuaran
{
    internal sealed class DeleteLuaranCommandHandler(
    ILuaranRepository luaranRepository,
    IPenelitianPkmRepository penelitianHibahRepository,
    IUnitOfWorkLuaran unitOfWork)
    : ICommandHandler<DeleteLuaranCommand>
    {
        public async Task<Result> Handle(DeleteLuaranCommand request, CancellationToken cancellationToken)
        {
            Domain.Luaran.Luaran? existingLuaran = await luaranRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);
            if (existingLuaran == null)
            {
                return Result.Failure<Guid>(LuaranErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianPkm), cancellationToken);
            if (existingPenelitianPkm == null)
            {
                return Result.Failure<Guid>(LuaranErrors.NotFoundHibah(Guid.Parse(request.UuidPenelitianPkm)));
            }

            if (existingLuaran?.PenelitianPkmId != existingPenelitianPkm?.Id)
            {
                return Result.Failure<Guid>(LuaranErrors.InvalidData());
            }

            await luaranRepository.DeleteAsync(existingLuaran!);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
