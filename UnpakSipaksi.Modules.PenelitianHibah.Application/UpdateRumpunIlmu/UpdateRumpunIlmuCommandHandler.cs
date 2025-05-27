using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;
using UnpakSipaksi.Modules.RumpunIlmu1.PublicApi;
using UnpakSipaksi.Modules.RumpunIlmu2.PublicApi;
using UnpakSipaksi.Modules.RumpunIlmu3.PublicApi;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateRumpunIlmu
{
    internal sealed class UpdateRumpunIlmuCommandHandler(
        IPenelitianHibahRepository penelitianHibahRepository,
        IRumpunIlmu1Api rumpunIlmu1Api,
        IRumpunIlmu2Api rumpunIlmu2Api,
        IRumpunIlmu3Api rumpunIlmu3Api,
        IUnitOfWorkHibah unitOfWork)
        : ICommandHandler<UpdateRumpunIlmuCommand>
    {
        public async Task<Result> Handle(UpdateRumpunIlmuCommand request, CancellationToken cancellationToken)
        {
            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianHibah), cancellationToken);

            var rumpunIlmu1 = await GetIfNotNull<RumpunIlmu1Response>(
                request.RumpunIlmu1Id, id1 => rumpunIlmu1Api.GetAsync(id1));

            var rumpunIlmu2 = await GetIfNotNull<RumpunIlmu2Response>(
                request.RumpunIlmu2Id, id2 => rumpunIlmu2Api.GetAsync(id2));

            var rumpunIlmu3 = await GetIfNotNull<RumpunIlmu3Response>(
                request.RumpunIlmu3Id, id3 => rumpunIlmu3Api.GetAsync(id3));

            if (rumpunIlmu1 is null && rumpunIlmu2 is null && rumpunIlmu3 is null)
                return Result.Failure<Guid>(PenelitianHibahErrors.InvalidInputRumpunIlmu());

            var createResult = Domain.PenelitianHibah.PenelitianHibah.UpdateRumpunIlmu(
                existingPenelitianHibah,
                ParseNullableId(rumpunIlmu1),
                ParseNullableId(rumpunIlmu2),
                ParseNullableId(rumpunIlmu3)
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
