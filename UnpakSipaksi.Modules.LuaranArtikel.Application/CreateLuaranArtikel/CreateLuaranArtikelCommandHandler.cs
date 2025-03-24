using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.LuaranArtikel.Application.Abstractions.Data;
using UnpakSipaksi.Modules.LuaranArtikel.Domain.LuaranArtikel;

namespace UnpakSipaksi.Modules.LuaranArtikel.Application.CreateLuaranArtikel
{
    internal sealed class CreateLuaranArtikelCommandHandler(
    ILuaranArtikelRepository LuaranArtikelRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateLuaranArtikelCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateLuaranArtikelCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.LuaranArtikel.LuaranArtikel> result = Domain.LuaranArtikel.LuaranArtikel.Create(
                request.Nama,
                request.Nilai
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            LuaranArtikelRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
