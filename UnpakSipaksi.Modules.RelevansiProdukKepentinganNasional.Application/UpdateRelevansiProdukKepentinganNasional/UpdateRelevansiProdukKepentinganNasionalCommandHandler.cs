using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Domain.RelevansiProdukKepentinganNasional;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.UpdateRelevansiProdukKepentinganNasional
{
    internal sealed class UpdateRelevansiProdukKepentinganNasionalCommandHandler(
    IRelevansiProdukKepentinganNasionalRepository RelevansiProdukKepentinganNasionalRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateRelevansiProdukKepentinganNasionalCommand>
    {
        public async Task<Result> Handle(UpdateRelevansiProdukKepentinganNasionalCommand request, CancellationToken cancellationToken)
        {
            Domain.RelevansiProdukKepentinganNasional.RelevansiProdukKepentinganNasional? existingRelevansiProdukKepentinganNasional = await RelevansiProdukKepentinganNasionalRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingRelevansiProdukKepentinganNasional is null)
            {
                Result.Failure(RelevansiProdukKepentinganNasionalErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.RelevansiProdukKepentinganNasional.RelevansiProdukKepentinganNasional> asset = Domain.RelevansiProdukKepentinganNasional.RelevansiProdukKepentinganNasional.Update(existingRelevansiProdukKepentinganNasional!)
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
