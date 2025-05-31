using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.LuaranArtikel.Application.Abstractions.Data;
using UnpakSipaksi.Modules.LuaranArtikel.Domain.LuaranArtikel;

namespace UnpakSipaksi.Modules.LuaranArtikel.Application.UpdateLuaranArtikel
{
    internal sealed class UpdateLuaranArtikelCommandHandler(
    ILuaranArtikelRepository LuaranArtikelRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateLuaranArtikelCommand>
    {
        public async Task<Result> Handle(UpdateLuaranArtikelCommand request, CancellationToken cancellationToken)
        {
            Domain.LuaranArtikel.LuaranArtikel? existingLuaranArtikel = await LuaranArtikelRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingLuaranArtikel is null)
            {
                return Result.Failure(LuaranArtikelErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.LuaranArtikel.LuaranArtikel> asset = Domain.LuaranArtikel.LuaranArtikel.Update(existingLuaranArtikel!)
                         .ChangeNama(request.Nama)
                         .ChangeNilai(request.Nilai)
                         .Build();

            if (asset.IsFailure)
            {
                return Result.Failure<Guid>(asset.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
