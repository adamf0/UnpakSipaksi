using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

using ITopikPenelitianApi = UnpakSipaksi.Modules.TopikPenelitian.PublicApi.ITopikPenelitianApi;
using TopikPenelitianResponseApi = UnpakSipaksi.Modules.TopikPenelitian.PublicApi.TopikPenelitianResponse;
using UnpakSipaksi.Modules.TopikPenelitian.Application.GetTopikPenelitian;

namespace UnpakSipaksi.Modules.TopikPenelitian.Infrastructure.PublicApi
{
    internal sealed class TopikPenelitianApi(ISender sender) : ITopikPenelitianApi
    {
        public async Task<TopikPenelitianResponseApi?> GetAsync(Guid TopikPenelitianUuid, CancellationToken cancellationToken = default)
        {
            Result<TopikPenelitianDefaultResponse> result = await sender.Send(new GetTopikPenelitianDefaultQuery(TopikPenelitianUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new TopikPenelitianResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.TemaPenelitianUuid,
                result.Value.Nama
            );
        }
    }

}
