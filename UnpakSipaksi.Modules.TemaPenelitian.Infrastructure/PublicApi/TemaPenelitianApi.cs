using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

using ITemaPenelitianApi = UnpakSipaksi.Modules.TemaPenelitian.PublicApi.ITemaPenelitianApi;
using TemaPenelitianResponseApi = UnpakSipaksi.Modules.TemaPenelitian.PublicApi.TemaPenelitianResponse;
using UnpakSipaksi.Modules.TemaPenelitian.Application.GetTemaPenelitian;

namespace UnpakSipaksi.Modules.TemaPenelitian.Infrastructure.PublicApi
{
    internal sealed class TemaPenelitianApi(ISender sender) : ITemaPenelitianApi
    {
        public async Task<TemaPenelitianResponseApi?> GetAsync(Guid TemaPenelitianUuid, CancellationToken cancellationToken = default)
        {
            Result<TemaPenelitianDefaultResponse> result = await sender.Send(new GetTemaPenelitianDefaultQuery(TemaPenelitianUuid), cancellationToken);

            if (result.IsFailure)
            {
                return null;
            }

            return new TemaPenelitianResponseApi(
                result.Value.Id,
                result.Value.Uuid,
                result.Value.FokusPenelitianUuid,
                result.Value.Nama
            );
        }
    }

}
