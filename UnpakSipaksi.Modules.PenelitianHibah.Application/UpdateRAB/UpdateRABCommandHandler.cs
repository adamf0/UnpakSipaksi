using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.RAB;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;
using UnpakSipaksi.Modules.KelompokRab.PublicApi;
using UnpakSipaksi.Modules.Satuan.PublicApi;
using UnpakSipaksi.Modules.Komponen.PublicApi;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateRAB
{
    internal sealed class UpdateRABCommandHandler(
        IRABRepository rabRepository,
        IPenelitianHibahRepository penelitianHibahRepository,
        IKelompokRabApi kelompokRabApi,
        IKomponenApi komponenApi,
        ISatuanApi satuanApi,
        IUnitOfWorkRAB unitOfWork)
        : ICommandHandler<UpdateRABCommand>
    {
        public async Task<Result> Handle(UpdateRABCommand request, CancellationToken cancellationToken)
        {
            Domain.RAB.RAB? existingRAB = await rabRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);
            if (existingRAB is null)
            {
                return Result.Failure(RABErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianHibah), cancellationToken);
            KelompokRabResponse? existingKelompokRAB = await kelompokRabApi.GetAsync(string.IsNullOrEmpty(request.UuidKelompokRab) ? Guid.Empty : Guid.Parse(request.UuidKelompokRab), cancellationToken);
            KomponenResponse? existingKomponen = await komponenApi.GetAsync(string.IsNullOrEmpty(request.UuidKomponen) ? Guid.Empty : Guid.Parse(request.UuidKomponen), cancellationToken);
            SatuanResponse? existingSatuan = await satuanApi.GetAsync(string.IsNullOrEmpty(request.UuidSatuan) ? Guid.Empty : Guid.Parse(request.UuidSatuan), cancellationToken);

            var createResult = Domain.RAB.RAB.Update(
                Guid.Parse(request.Uuid),
                Guid.Parse(request.UuidPenelitianHibah),
                existingPenelitianHibah,
                existingRAB,
                string.IsNullOrEmpty(existingKelompokRAB?.Id) ? null : int.Parse(existingKelompokRAB.Id),
                string.IsNullOrEmpty(existingKomponen?.Id) ? null : int.Parse(existingKomponen.Id),
                request.Item,
                string.IsNullOrEmpty(existingSatuan?.Id) ? null : int.Parse(existingSatuan.Id),
                request.HargaSatuan,
                request.Total
            );

            if (createResult.IsFailure)
                return Result.Failure<Guid>(createResult.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
