using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Domain.RelevansiProdukKepentinganNasional;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Application.DeleteRelevansiProdukKepentinganNasional
{
    internal sealed class DeleteRelevansiProdukKepentinganNasionalCommandHandler(
    IRelevansiProdukKepentinganNasionalRepository RelevansiProdukKepentinganNasionalRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteRelevansiProdukKepentinganNasionalCommand>
    {
        public async Task<Result> Handle(DeleteRelevansiProdukKepentinganNasionalCommand request, CancellationToken cancellationToken)
        {
            Domain.RelevansiProdukKepentinganNasional.RelevansiProdukKepentinganNasional? existingRelevansiProdukKepentinganNasional = await RelevansiProdukKepentinganNasionalRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingRelevansiProdukKepentinganNasional is null)
            {
                return Result.Failure(RelevansiProdukKepentinganNasionalErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await RelevansiProdukKepentinganNasionalRepository.DeleteAsync(existingRelevansiProdukKepentinganNasional!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
