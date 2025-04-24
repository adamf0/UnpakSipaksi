using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPenelitian.PublicApi;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;
using UnpakSipaksi.Modules.PrioritasRiset.PublicApi;
using UnpakSipaksi.Modules.TemaPenelitian.PublicApi;
using UnpakSipaksi.Modules.TopikPenelitian.PublicApi;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateRiset
{
    internal sealed class UpdateRisetCommandHandler(
        IPenelitianHibahRepository penelitianHibahRepository,
        IPrioritasRisetApi prioritasRisetApi,
        IFokusPenelitianApi fokusPenelitianApi,
        ITemaPenelitianApi temaPenelitianApi,
        ITopikPenelitianApi topikPenelitianApi,
        IUnitOfWorkHibah unitOfWork)
        : ICommandHandler<UpdateRisetCommand>
    {
        public async Task<Result> Handle(UpdateRisetCommand request, CancellationToken cancellationToken)
        {
            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            var prioritasRiset = await GetIfNotNull<PrioritasRisetResponse>(
                request.PrioritasRiset, id => prioritasRisetApi.GetAsync(id));

            var fokusPenelitian = await GetIfNotNull<FokusPenelitianResponse>(
                request.BidangFokusPenelitianId, id => fokusPenelitianApi.GetAsync(id));

            if (prioritasRiset is null || fokusPenelitian is null)
                return Result.Failure<Guid>(PenelitianHibahErrors.InvalidInputPrioritasFokus());

            var temaPenelitian = await GetIfNotNull<TemaPenelitianResponse>(
                request.BidangFokusPenelitianTemaId, id => temaPenelitianApi.GetAsync(id));

            var topikPenelitian = await GetIfNotNull<TopikPenelitianResponse>(
                request.BidangFokusPenelitianTemaTopikId, id => topikPenelitianApi.GetAsync(id));

            
            var createResult = Domain.PenelitianHibah.PenelitianHibah.UpdateRiset(
                existingPenelitianHibah,
                ParseNullableId(prioritasRiset),
                ParseNullableId(fokusPenelitian),
                ParseNullableId(temaPenelitian),
                ParseNullableId(topikPenelitian)
            );

            if (createResult.IsFailure)
                return Result.Failure<Guid>(createResult.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        private static async Task<T?> GetIfNotNull<T>(string? id, Func<Guid, Task<T?>> fetchFunc) where T : class
        {
            return string.IsNullOrEmpty(id) ? null : await fetchFunc(Guid.Parse(id));
        }

        private static int? ParseNullableId(dynamic? response)
        {
            return response == null ? null : int.Parse(response.Id);
        }
    }
}
