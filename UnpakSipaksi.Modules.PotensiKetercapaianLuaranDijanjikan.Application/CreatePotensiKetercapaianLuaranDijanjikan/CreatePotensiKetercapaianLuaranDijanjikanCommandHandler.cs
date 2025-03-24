using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Domain.PotensiKetercapaianLuaranDijanjikan;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Application.CreatePotensiKetercapaianLuaranDijanjikan
{
    internal sealed class CreatePotensiKetercapaianLuaranDijanjikanCommandHandler(
    IPotensiKetercapaianLuaranDijanjikanRepository PotensiKetercapaianLuaranDijanjikanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePotensiKetercapaianLuaranDijanjikanCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreatePotensiKetercapaianLuaranDijanjikanCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.PotensiKetercapaianLuaranDijanjikan.PotensiKetercapaianLuaranDijanjikan> result = Domain.PotensiKetercapaianLuaranDijanjikan.PotensiKetercapaianLuaranDijanjikan.Create(
                request.Nama,
                request.BobotPDP,
                request.BobotTerapan,
                request.BobotKerjasama,
                request.BobotPenelitianDasar,
                request.Skor
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            PotensiKetercapaianLuaranDijanjikanRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
