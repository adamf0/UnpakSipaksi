using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.LuaranArtikel.Application.Abstractions.Data;
using UnpakSipaksi.Modules.LuaranArtikel.Domain.LuaranArtikel;

namespace UnpakSipaksi.Modules.LuaranArtikel.Application.DeleteLuaranArtikel
{
    internal sealed class DeleteLuaranArtikelCommandHandler(
    ILuaranArtikelRepository LuaranArtikelRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteLuaranArtikelCommand>
    {
        public async Task<Result> Handle(DeleteLuaranArtikelCommand request, CancellationToken cancellationToken)
        {
            Domain.LuaranArtikel.LuaranArtikel? existingLuaranArtikel = await LuaranArtikelRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingLuaranArtikel is null)
            {
                return Result.Failure(LuaranArtikelErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await LuaranArtikelRepository.DeleteAsync(existingLuaranArtikel!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
