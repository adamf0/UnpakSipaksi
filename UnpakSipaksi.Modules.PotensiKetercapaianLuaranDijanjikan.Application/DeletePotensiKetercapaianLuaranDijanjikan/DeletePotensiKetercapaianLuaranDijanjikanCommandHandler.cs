using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Domain.PotensiKetercapaianLuaranDijanjikan;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.DeletePotensiKetercapaianLuaranDijanjikan
{
    internal sealed class DeletePotensiKetercapaianLuaranDijanjikanCommandHandler(
    IPotensiKetercapaianLuaranDijanjikanRepository PotensiKetercapaianLuaranDijanjikanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeletePotensiKetercapaianLuaranDijanjikanCommand>
    {
        public async Task<Result> Handle(DeletePotensiKetercapaianLuaranDijanjikanCommand request, CancellationToken cancellationToken)
        {
            Domain.PotensiKetercapaianLuaranDijanjikan.PotensiKetercapaianLuaranDijanjikan? existingPotensiKetercapaianLuaranDijanjikan = await PotensiKetercapaianLuaranDijanjikanRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingPotensiKetercapaianLuaranDijanjikan is null)
            {
                return Result.Failure(PotensiKetercapaianLuaranDijanjikanErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await PotensiKetercapaianLuaranDijanjikanRepository.DeleteAsync(existingPotensiKetercapaianLuaranDijanjikan!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
