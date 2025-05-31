using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Domain.PotensiKetercapaianLuaranDijanjikan;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.UpdatePotensiKetercapaianLuaranDijanjikan
{
    internal sealed class UpdatePotensiKetercapaianLuaranDijanjikanCommandHandler(
    IPotensiKetercapaianLuaranDijanjikanRepository PotensiKetercapaianLuaranDijanjikanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdatePotensiKetercapaianLuaranDijanjikanCommand>
    {
        public async Task<Result> Handle(UpdatePotensiKetercapaianLuaranDijanjikanCommand request, CancellationToken cancellationToken)
        {
            Domain.PotensiKetercapaianLuaranDijanjikan.PotensiKetercapaianLuaranDijanjikan? existingPotensiKetercapaianLuaranDijanjikan = await PotensiKetercapaianLuaranDijanjikanRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingPotensiKetercapaianLuaranDijanjikan is null)
            {
                return Result.Failure(PotensiKetercapaianLuaranDijanjikanErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.PotensiKetercapaianLuaranDijanjikan.PotensiKetercapaianLuaranDijanjikan> asset = Domain.PotensiKetercapaianLuaranDijanjikan.PotensiKetercapaianLuaranDijanjikan.Update(existingPotensiKetercapaianLuaranDijanjikan!)
                         .ChangeNama(request.Nama)
                         .ChangeSkor(request.Skor)
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
