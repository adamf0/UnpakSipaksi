using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriLuaran.PublicApi;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.Luaran;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteLuaran
{
    internal sealed class DeleteLuaranCommandHandler(
    ILuaranRepository luaranRepository,
    IPenelitianHibahRepository penelitianHibahRepository,
    IUnitOfWorkLuaran unitOfWork)
    : ICommandHandler<DeleteLuaranCommand>
    {
        //[PR] ini masih 2 query, seharusnya jadi 1 query atau mungkin ini benar sisanya salah. i dont know
        public async Task<Result> Handle(DeleteLuaranCommand request, CancellationToken cancellationToken)
        {
            Domain.Luaran.Luaran? existingLuaran = await luaranRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);
            if (existingLuaran != null)
            {
                return Result.Failure<Guid>(LuaranErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianHibah), cancellationToken);
            if (existingPenelitianHibah != null)
            {
                return Result.Failure<Guid>(LuaranErrors.NotFoundHibah(Guid.Parse(request.UuidPenelitianHibah)));
            }

            if (existingLuaran?.PenelitianHibahId != existingPenelitianHibah?.Id) {
                return Result.Failure<Guid>(LuaranErrors.InvalidData());
            }

            await luaranRepository.DeleteAsync(existingLuaran!);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
