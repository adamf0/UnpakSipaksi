using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Domain.RelevansiProdukKepentinganNasional;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.CreateRelevansiProdukKepentinganNasional
{
    internal sealed class CreateRelevansiProdukKepentinganNasionalCommandHandler(
    IRelevansiProdukKepentinganNasionalRepository RelevansiProdukKepentinganNasionalRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRelevansiProdukKepentinganNasionalCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateRelevansiProdukKepentinganNasionalCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.RelevansiProdukKepentinganNasional.RelevansiProdukKepentinganNasional> result = Domain.RelevansiProdukKepentinganNasional.RelevansiProdukKepentinganNasional.Create(
                request.Nama,
                request.Skor
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            RelevansiProdukKepentinganNasionalRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}
